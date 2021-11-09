using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MentorsBlog.Core.DataAccess.Intefaces;
using MentorsBlog.Core.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MentorsBlog.Core.DataAccess
{
    public sealed class DataContext : IdentityDbContext, IDataContext
    {
        private readonly ILogger<DataContext> _logger;

        public DataContext(DbContextOptions options, 
            ILogger<DataContext> logger)
            : base(options)
        {
            _logger = logger;
            Database.Migrate();
        }

        public IQueryable<T> GetQueryable<T>(bool trackChanges = true, bool disabled = false) where T : class, new()
        {
            return GetQueryable<T>(null, trackChanges);
        }

        private IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
            where T : class, new()
        {
            var query = GetQueryableNonAudit(expression, trackChanges);

            return query;
        }

        private IQueryable<T> GetQueryableNonAudit<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
            where T : class, new()
        {
            var query = trackChanges
                ? Set<T>().AsQueryable()
                : Set<T>().AsNoTracking();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }
        
        public T Delete<T>(T item) where T : class, new()
        {
            return Set<T>().Remove(item).Entity;
        }
        
        public void DeleteRange<T>(IEnumerable<T> item) where T : class, new()
        {
            Set<T>().RemoveRange(item);
        }

        public T Insert<T>(T item) where T : class, new()
        {
            return PerformAction(item, EntityState.Added);
        }

        public IEnumerable<T> InsertMany<T>(IEnumerable<T> items) where T : class, new()
        {
            return items.Select(item => PerformAction(item, EntityState.Added)).ToList();
        }
        
        public new T Update<T>(T item) where T : class, new()
        {
            return PerformAction(item, EntityState.Modified);
        }

        public IEnumerable<T> UpdateMany<T>(IEnumerable<T> items) where T : class, new()
        {
            return items.Select(item => PerformAction(item, EntityState.Modified)).ToList();
        }

        private TItem PerformAction<TItem>(TItem item, EntityState entityState) where TItem : class, new()
        {
            Entry(item).State = entityState;
            return item;
        }

        public int Save()
        {
            var changes = 0;
            try
            {
                AddTimestamps();
                changes = SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Database save error");
                throw;
            }
            return changes;
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && x.State is EntityState.Added or EntityState.Modified);

            foreach (var entity in entities)
            {
                var now = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}