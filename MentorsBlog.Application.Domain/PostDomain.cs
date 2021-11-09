using System;
using System.Linq;
using MentorsBlog.Application.Domain.Interfaces;
using MentorsBlog.Core.DataAccess.Intefaces;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Domain
{
    public class PostDomain : IPostDomain
    {
        private readonly IDataContext _dataContext;

        public PostDomain(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbPost Create(DbPost dbPost)
        {
            if(dbPost == null)
            {
                return null;
            }

            var dbPostInserted = _dataContext.Insert(dbPost);
            
            if (dbPostInserted == null)
            {
                return null;
            }
            
            return _dataContext.Save() != 0 ? dbPostInserted : null;
        }

        public IQueryable<DbPost> Get(bool trackChanges = true)
        {
            return _dataContext.GetQueryable<DbPost>(trackChanges);
        }
        
        public DbPost Get(Guid id)
        {
            return Get().FirstOrDefault(x => x.Id == id);
        }

        public DbPost Update(DbPost dbPost)
        {
            if (dbPost == null)
            {
                return null;
            }

            var dbPostUpdated = _dataContext.Update(dbPost);

            if (dbPostUpdated == null)
            {
                return null;
            }

            return _dataContext.Save() != 0 ? dbPostUpdated : null;
        }

        public bool Delete(Guid id)
        {
            var dbPost = Get(id);
            if (dbPost == null)
            {
                return false;
            }

            _dataContext.Delete(dbPost);

            return _dataContext.Save() != 0;
        }
    }
}