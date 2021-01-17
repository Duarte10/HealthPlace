using HealthPlace.Core.Models;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Mappers
{
    /// <summary>
    /// Maps a User to a UserModel and vice-versa
    /// </summary>
    internal static class UserMapper
    {
        /// <summary>
        /// Maps a UserModel to User.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>A user</returns>
        public static User ToUser(this UserModel userModel)
        {
            return new User()
            {
                Id = userModel.Id,
                CreatedBy = userModel.CreatedBy,
                CreatedOn = userModel.CreatedOn,
                UpdatedBy = userModel.UpdatedBy,
                UpdatedOn = userModel.UpdatedOn,
                Email = userModel.Email,
                IsActive = userModel.IsActive,
                Name = userModel.Name,
                PasswordHash = userModel.PasswordHash
            };
        }

        /// <summary>
        /// Maps a User to UserModel.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A UserModel</returns>
        public static UserModel ToUserModel(this User user)
        {
            return new UserModel()
            {
                Id = user.Id,
                CreatedBy = user.CreatedBy,
                CreatedOn = user.CreatedOn,
                UpdatedBy = user.UpdatedBy,
                UpdatedOn = user.UpdatedOn,
                Email = user.Email,
                IsActive = user.IsActive,
                Name = user.Name,
                PasswordHash = user.PasswordHash
            };
        }
    }
}
