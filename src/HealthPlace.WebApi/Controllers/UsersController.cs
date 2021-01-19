using System;
using HealthPlace.Logic.Exceptions;
using HealthPlace.WebApi.ApiResources;
using HealthPlace.WebApi.Helpers;
using HealthPlace.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlace.WebApi.Controllers
{
    /// <summary>
    /// ApiController of the User entity
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>HttpResponse with the generated JWT Token</returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            string token = _userService.Authenticate(model);

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>All the users.</returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
            catch
            {
                return Problem();
            }
        }

        /// <summary>
        /// Retrieves the user with the specified id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>The user</returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetVisitor(Guid id)
        {
            try
            {
                return Ok(_userService.GetById(id));
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpPost("update")]
        public IActionResult Update(UserResource user)
        {
            try
            {
                string updatedBy = ((UserResource)HttpContext.Items["User"]).Email;
                _userService.UpdateUser(user, updatedBy);
                return Ok();
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <returns>HttpResponse</returns>
        [Authorize]
        [HttpPost("new")]
        public IActionResult New(NewUserResource newUser)
        {
            try
            {
                newUser.CreatedBy = ((UserResource)HttpContext.Items["User"]).Email;
                _userService.CreateUser(newUser);
                return Ok();
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>HttpResponse</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var user = _userService.GetById(id);
                if (user == null)
                {
                    return BadRequest("Invalid user id.");
                }
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return Problem();
            }
        }

    }
}
