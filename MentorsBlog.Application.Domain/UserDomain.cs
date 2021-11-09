using System;
using System.Linq;
using MentorsBlog.Application.Domain.Interfaces;
using MentorsBlog.Core.DataAccess.Intefaces;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Domain
{
    public class UserDomain : IUserDomain
    {
        private readonly IDataContext _dataContext;

        public UserDomain(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbUser Create(DbUser dbUser)
        {
            if(dbUser == null)
            {
                return null;
            }

            var dbUserInserted = _dataContext.Insert(dbUser);
            
            if (dbUserInserted == null)
            {
                return null;
            }
            
            return _dataContext.Save() != 0 ? dbUserInserted : null;
        }

        public DbUser Get(Guid id)
        {
            return Get().FirstOrDefault(x => x.Id == id);
        }
        
        public IQueryable<DbUser> Get(bool trackChanges = true)
        {
            return _dataContext.GetQueryable<DbUser>(trackChanges);
        }
    }
}