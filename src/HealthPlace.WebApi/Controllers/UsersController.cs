using HealthPlace.Logic.Exceptions;
using HealthPlace.Logic.Helpers;
using HealthPlace.Logic.Managers;
using HealthPlace.WebApi.ApiResources;
using HealthPlace.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using static HealthPlace.WebApi.Services.UsersService;

namespace HealthPlace.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("hash")]
        public IActionResult Hash(string clearText)
        {
            var hashingHelper = new HashingHelper();
            return Ok(hashingHelper.HashToString(clearText));
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            string token = _userService.Authenticate(model);

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }

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
            catch
            {
                return Problem();
            }
        }
    }
}
