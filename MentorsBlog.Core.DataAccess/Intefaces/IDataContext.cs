using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MentorsBlog.Core.DataAccess.Intefaces
{
    public interface IDataContext
    {
        IQueryable<T> GetQueryable<T>(bool trackChanges = true, bool disabled = false)
            where T : class, new();

        T Insert<T>(T item) where T : class, new();

        IEnumerable<T> InsertMany<T>(IEnumerable<T> items) where T : class, new();

        T Update<T>(T item) where T : class, new();

        IEnumerable<T> UpdateMany<T>(IEnumerable<T> items) where T : class, new();

        int Save();

        T Delete<T>(T item) where T : class, new();

        void DeleteRange<T>(IEnumerable<T> item) where T : class, new();

        void DetachAllEntities();
    }
}