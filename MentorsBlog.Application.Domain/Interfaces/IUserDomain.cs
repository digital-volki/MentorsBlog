using System;
using System.Linq;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Domain.Interfaces
{
    public interface IUserDomain
    {
        DbUser Create(DbUser dbUser);
        DbUser Get(Guid id);
        IQueryable<DbUser> Get(bool trackChanges = true);
    }
}