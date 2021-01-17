using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HealthPlace.Logic.Managers;
using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;
using HealthPlace.WebApi.Helpers;
using HealthPlace.WebApi.Resources.Mappers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HealthPlace.WebApi.Services
{
    public class UsersService
    {
        public interface IUserService
        {
            string Authenticate(AuthenticateRequest model);
            UserResource GetById(Guid id);

            public Guid CreateUser(NewUserResource newUser);
        }

        public class UserService : IUserService
        {
            #region Private Properties

            private readonly AppSettings _appSettings;

            #endregion Private Properties

            public UserService(IOptions<AppSettings> appSettings)
            {
                _appSettings = appSettings.Value;
            }


            #region Public Methods

            public string Authenticate(AuthenticateRequest model)
            {
                UserManager userMng = new UserManager();

                bool authenticate = userMng.Authenticate(model.Email, model.Password);
                if (!authenticate) return null;

                User user = userMng.GetRecordByEmail(model.Email);

                // authentication successful so generate jwt token
                var token = GenerateJwtToken(user.ToUserResource());

                return token;
            }

            public Guid CreateUser(NewUserResource newUser)
            {
                User user = new User()
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    PasswordHash = newUser.Password,
                    CreatedBy = newUser.CreatedBy,
                    UpdatedBy = newUser.CreatedBy,
                    IsActive = true
                };

                return new UserManager().Insert(user);
            }

            public UserResource GetById(Guid id)
            {
                UserManager userMng = new UserManager();
                var user = userMng.GetRecordById(id);
                return user.ToUserResource();
            }

            #endregion Public Methods

            #region Private Methods

            private string GenerateJwtToken(UserResource user)
            {
                // generate token that is valid for 7 days
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddHours(_appSettings.JwtDurationHours),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            #endregion Private Methods
        }
    }
}
