using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HomeSecurityAPI.Interfaces;
using HomeSecurityAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HomeSecurityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        //GET api/user
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());

        }

        //GET api/user
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            return Ok(await _userService.GetbyUsername(username));
        }
    }
}