using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HealthPlace.Logic.Managers;
using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;
using HealthPlace.WebApi.Helpers;
using HealthPlace.WebApi.Resources.Mappers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HealthPlace.WebApi.Services
{
    /// <summary>
    /// Defines the methods implemented by the UserService
    /// </summary>
    public interface IUserService
    {
        string Authenticate(AuthenticateRequest model);
        UserResource GetById(Guid id);
        List<UserResource> GetAll();
        Guid CreateUser(NewUserResource newUser);
        void DeleteUser(Guid id);
    }


    /// <summary>
    /// Service with user operations
    /// </summary>
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

        /// <summary>
        /// Authenticates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A JWT auth token</returns>
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

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves all the users.
        /// </summary>
        /// <returns>All the users</returns>
        public List<UserResource> GetAll()
        {
            UserManager userMng = new UserManager();
            return userMng.GetAllRecords().Select(u => u.ToUserResource()).ToList();
        }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The user</returns>
        public UserResource GetById(Guid id)
        {
            UserManager userMng = new UserManager();
            var user = userMng.GetRecordById(id);
            return user.ToUserResource();
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The user id.</param>
        public void DeleteUser(Guid id)
        {
            UserManager userMng = new UserManager();
            userMng.Delete(id);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="user">The user resource.</param>
        /// <returns>A Jwt token</returns>
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
