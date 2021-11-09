using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MentorsBlog.Core.Common.Interfaces;
using MentorsBlog.Core.Common.Models;
using MentorsBlog.Core.DataAccess.Models;
using Microsoft.IdentityModel.Tokens;

namespace MentorsBlog.Core.Web.Auth
{
    public class JwtTokenGenerator
    {
        private readonly ISettings _settings;

        private readonly Tokens _tokens;

        public JwtTokenGenerator(ISettings settings)
        {
            _settings = settings;
            _tokens = settings.GetSection<AppSettings>($"{nameof(AppSettings)}").Tokens;
        }

        public string GenerateAuth(DbUser user)
        {
            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nickname)
            };

            var jwt = new JwtSecurityToken(
                issuer: _tokens.Authenticate.Issuer,
                audience: _tokens.Authenticate.Audience,
                notBefore: DateTime.UtcNow,
                claims: claimsList,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_tokens.Authenticate.Ttl)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(_tokens.Authenticate.Secret)),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}