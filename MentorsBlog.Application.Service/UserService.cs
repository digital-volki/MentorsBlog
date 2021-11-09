using System.Linq;
using MentorsBlog.Application.Domain.Interfaces;
using MentorsBlog.Application.Service.Interfaces;
using MentorsBlog.Application.Service.Mappers;
using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Web.Auth;

namespace MentorsBlog.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDomain _userDomain;
        private readonly Authenticator _authenticator;

        public UserService(IUserDomain userDomain, Authenticator authenticator)
        {
            _userDomain = userDomain;
            _authenticator = authenticator;
        }

        public bool IsUserExist(string nickname)
        {
            return _userDomain.Get().Any(x => x.Nickname == nickname);
        }

        public string Registration(User user)
        {
            if (user == null)
            {
                return null;
            }

            return _userDomain.Create(user.ToDbUser()) != null 
                ? _authenticator.AuthByCredentials(user.ToTokenRequest())
                : null;
        }

        public string Authorization(User user)
        {
            return _authenticator.AuthByCredentials(user.ToTokenRequest());
        }
    }
}