using System;
using System.Collections.Generic;
using System.Linq;
using HealthPlace.Core.Database;
using HealthPlace.Core.Models;
using HealthPlace.Logic.Exceptions;
using HealthPlace.Logic.Helpers;
using HealthPlace.Logic.Mappers;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Managers
{
    /// <summary>
    /// User Manager (CRUD operations for User object)
    /// </summary>
    public class UserManager : IManager<User>
    {

        #region CRUD Operations

        /// <summary>
        /// Gets all the users stored in the database
        /// </summary>
        /// <returns>The collection of users</returns>
        public IEnumerable<User> GetAllRecords()
        {
            var users = DbContext<UserModel>.GetAllRecords();

            // excule system user
            return users.Where(u => u.Name != "<system>").Select(x => x.ToUser()).ToList();
        }

        /// <summary>
        /// Retrieves the user with the specified Id
        /// </summary>
        /// <param name="id">The Id of the user</param>
        /// <returns>The user with the specified id</returns>
        public User GetRecordById(Guid id)
        {
            var user = DbContext<UserModel>.GetRecordById(id).ToUser();
            if (user == null)
                throw new EntityNotFoundException("User", "id");
            return user;
        }

        /// <summary>
        /// Gets the record by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="throwIfNotFound">Throws an exception if the user is not found.</param>
        /// <returns>The user</returns>
        /// <exception cref="EntityNotFoundException">User - email</exception>
        public User GetRecordByEmail(string email, bool throwIfNotFound = false)
        {
            var users = DbContext<UserModel>.GetAllRecords().ToList();
            var user = users.FirstOrDefault(u => u.Email == email);
            if (user == null && throwIfNotFound)
                throw new EntityNotFoundException("User", "email");
            return user?.ToUser();
        }

        /// <summary>
        /// Inserts a new user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The inserted user Id</returns>
        public Guid Insert(User user)
        {
            // Hash password
            HashingHelper hashingHelper = new HashingHelper();
            user.PasswordHash = hashingHelper.HashToString(user.PasswordHash);
            this.Validate(user);
            return DbContext<UserModel>.Insert(user.ToUserModel());
        }

        /// <summary>
        /// Updates a user record
        /// </summary>
        /// <param name="user">The user record</param>
        public void Update(User user)
        {
            this.Validate(user);
            DbContext<UserModel>.Update(user.ToUserModel());
        }

        /// <summary>
        /// Deletes a user record
        /// </summary>
        /// <param name="id">The user record Id</param>
        public void Delete(Guid id)
        {
            // Delete record 
            DbContext<UserModel>.Delete(id);
        }

        #endregion CRUD Operation

        public bool Authenticate(string email, string password)
        {
            User user = this.GetRecordByEmail(email, true);
            HashingHelper hashingHelper = new HashingHelper();
            return hashingHelper.Verify(password, user.PasswordHash);
        }

        #region Private methods

        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="user">The user record</param>
        private void Validate(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
                throw new EntityValidationException("The user name cannot be empty!");

            if (string.IsNullOrEmpty(user.Email))
                throw new EntityValidationException("The user email cannot be empty!");

            if (string.IsNullOrEmpty(user.PasswordHash))
                throw new EntityValidationException("The user password hash cannot be empty!");

            var userWithSameEmail = this.GetRecordByEmail(user.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != user.Id)
                throw new EntityValidationException($"There is already a user with the email: {user.Email}");
        }
        #endregion Private methods

    }
}
