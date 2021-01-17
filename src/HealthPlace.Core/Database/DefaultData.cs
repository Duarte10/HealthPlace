using System;
using HealthPlace.Core.Models;

namespace HealthPlace.Core.Database
{
    internal static class DefaultData
    {
        internal static UserModel[] GetDefaultUsers()
        {
            return new UserModel[]
            {
                new UserModel()
                {
                    Id = new Guid("eef97486-213c-4b2f-86c9-8db81021a9e6"),
                    Name = "<system>",
                    Email = "<system>",
                    PasswordHash = "AQAQJwAABO6h5EsNuF0H0WQcKVxOjSG0I5anYilJWgdr/M5hL8dXQGadasYfbPTqukn9pIgs", // admin
                    CreatedBy = "<system>",
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = "<system>",
                    UpdatedOn = DateTime.UtcNow,
                    IsActive = true
                },
                new UserModel()
                {
                    Id = new Guid("102d2d73-0eba-4573-9dce-297eb671139c"),
                    Name = "Administrator",
                    Email = "admin@healthplace.com",
                    PasswordHash = "AQAQJwAABO6h5EsNuF0H0WQcKVxOjSG0I5anYilJWgdr/M5hL8dXQGadasYfbPTqukn9pIgs", // admin
                    CreatedBy = "<system>",
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = "<system>",
                    UpdatedOn = DateTime.UtcNow,
                    IsActive = true
                }
            };
        }
    }
}
