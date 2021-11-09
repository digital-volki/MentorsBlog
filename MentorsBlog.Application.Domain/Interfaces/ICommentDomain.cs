using System;
using System.Linq;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Domain.Interfaces
{
    public interface ICommentDomain
    {
        DbComment Create(DbComment dbComment);
        DbComment Get(Guid id);
        IQueryable<DbComment> Get(bool trackChanges = true);
        bool Delete(Guid id);
    }
}