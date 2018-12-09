using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HomeSecurityAPI.Interfaces;
using HomeSecurityAPI.Models;

namespace HomeSecurityAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
            private IUserService _userService;

            public UserController(IUserService userService)
            {
                _userService = userService;
            }

            [AllowAnonymous]
            [HttpPost("authenticate")]
            public IActionResult Authenticate([FromBody]User userParam)
            {
                var user = _userService.Authenticate(userParam.Username, userParam.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(user);
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
        }
    }