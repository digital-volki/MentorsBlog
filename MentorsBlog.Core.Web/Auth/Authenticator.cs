using System.Linq;
using MentorsBlog.Application.Domain.Interfaces;
using MentorsBlog.Core.Common;
using MentorsBlog.Core.Web.Auth.Models;

namespace MentorsBlog.Core.Web.Auth
{
    public class Authenticator
    {
        private readonly IUserDomain _userDomain;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        public Authenticator(
            JwtTokenGenerator jwtAuthenticator,
            IUserDomain userDomain)
        {
            _userDomain = userDomain;
            _jwtTokenGenerator = jwtAuthenticator;
        }

        public string AuthByCredentials(TokenRequest token)
        {
            return Authorize(token.Login, token.Password);
        }

        private string Authorize(string nickname, string password)
        {
            var dbUser = _userDomain.Get().FirstOrDefault(x => x.Nickname == nickname);
            if (dbUser == null)
            {
                return string.Empty;
            }

            return ValidatePassword(password, dbUser.PasswordHash) 
                ? _jwtTokenGenerator.GenerateAuth(dbUser) 
                : string.Empty;
        }

        private static bool ValidatePassword(string password, string passwordHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordHash))
            {
                return false;
            }

            return  passwordHash == password.HashToSHA256();
        }
    }
}