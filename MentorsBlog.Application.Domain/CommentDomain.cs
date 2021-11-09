using System;
using System.Linq;
using MentorsBlog.Application.Domain.Interfaces;
using MentorsBlog.Core.DataAccess.Intefaces;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Domain
{
    public class CommentDomain : ICommentDomain
    {
        private readonly IDataContext _dataContext;

        public CommentDomain(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbComment Create(DbComment dbComment)
        {
            if(dbComment == null)
            {
                return null;
            }

            var dbCommentInserted = _dataContext.Insert(dbComment);
            
            if (dbCommentInserted == null)
            {
                return null;
            }
            
            return _dataContext.Save() != 0 ? dbCommentInserted : null;
        }

        public DbComment Get(Guid id)
        {
            return Get().FirstOrDefault(x => x.Id == id);
        }
        
        public IQueryable<DbComment> Get(bool trackChanges = true)
        {
            return _dataContext.GetQueryable<DbComment>(trackChanges);
        }

        public bool Delete(Guid id)
        {
            var dbComment = Get(id);
            if (dbComment == null)
            {
                return false;
            }

            _dataContext.Delete(dbComment);

            return _dataContext.Save() != 0;
        }
    }
}