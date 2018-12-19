using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using HomeSecurityAPI.Interfaces;
using HomeSecurityAPI.Models;
using HomeSecurityAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HomeSecurityAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private IUserService _userService;
        private readonly TokenService _tokenService;

        public AuthenticateController(IUserService userService, IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
            _tokenService = new TokenService(
                _appSettings.Secret,
                double.Parse(configuration["JwtExpireDays"]),
                configuration["JwtIssuer"]);
        }

        
        //POST api/authenticate
        [HttpPost]
        public async Task<ActionResult<string>> Authenticate([FromBody]User u)
        {
            try
            {
                /*var u = new User {
                    Username = Username,
                    Password = Password
                };*/
                var user = await _userService.Authenticate(u);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                var token = _tokenService.GenerateJwtToken(user);
            return Ok(token);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case AuthenticationException _:
                        return StatusCode(401, e.Message);
                    case InvalidDataException _:
                        return StatusCode(404, e.Message);
                    default:
                        return StatusCode(500, e.Message);
                }
            }
        }
    }
}