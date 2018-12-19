using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSecurityAPI.Interfaces;
using HomeSecurityAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeSecurityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IUserService _userService;
        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User u)
        {
            try
            {
                return Ok(await _userService.Create(u));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            // needs info check and exception handling
           
        }
    }
}