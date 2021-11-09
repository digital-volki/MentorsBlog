using System;
using System.Linq;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Domain.Interfaces
{
    public interface IPostDomain
    {
        DbPost Create(DbPost dbPost);
        DbPost Get(Guid id);
        IQueryable<DbPost> Get(bool trackChanges = true);
        DbPost Update(DbPost dbPost);
        bool Delete(Guid id);
    }
}