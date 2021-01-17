using HealthPlace.Core.Models;
using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;

namespace HealthPlace.WebApi.Resources.Mappers
{
    /// <summary>
    /// Maps a User to a UserResource and vice-versa
    /// </summary>
    internal static class UserMapper
    {
        /// <summary>
        /// Maps a User to a UserResource.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A user</returns>
        public static UserResource ToUserResource(this User user)
        {
            return new UserResource()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PasswordHash = user.PasswordHash
            };
        }

        /// <summary>
        /// Maps a User to UserModel.
        /// </summary>
        /// <param name="userResource">The user.</param>
        /// <returns>A User</returns>
        public static User ToUser(this UserResource userResource)
        {
            return new User()
            {
                Id = userResource.Id,
                Email = userResource.Email,
                Name = userResource.Name,
                PasswordHash = userResource.PasswordHash
            };
        }
    }
}
