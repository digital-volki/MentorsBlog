using System.Collections.Generic;
using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Common;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Core.DataAccess.Models;
using MentorsBlog.Core.Web.Auth.Models;

namespace MentorsBlog.Application.Service.Mappers
{
    internal static class UserMappers
    {
        #region Descending
        
        internal static DbUser ToDbUser(this User source)
        {
            return source == null ? default : new DbUser
            {
                Id = source.Id,
                Nickname = source.Nickname,
                PasswordHash = source.Password.HashToSHA256()
            };
        }
        
        internal static TokenRequest ToTokenRequest(this User source)
        {
            return source == null ? default : new TokenRequest
            {
                Login = source.Nickname,
                Password = source.Password
            };
        }

        #endregion
    }
}