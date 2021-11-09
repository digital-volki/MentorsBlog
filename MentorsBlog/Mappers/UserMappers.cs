using System;
using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Models.Responses;
using System.Collections.Generic;
using MentorsBlog.Models.Requests;

namespace MentorsBlog.Mappers
{
    internal static class UserMappers
    {
        #region Descending
        
        internal static User ToUser(this RequestAuthorize source)
        {
            return source == null ? default : new User
            {
                Nickname = source.Nickname,
                Password = source.Password,
            };
        }
        
        internal static User ToUser(this RequestRegistration source)
        {
            return source == null ? default : new User
            {
                Nickname = source.Nickname,
                Password = source.Password,
            };
        }

        #endregion
    }
}