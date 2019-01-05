using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSecurityAPI.DataAccess;
using HomeSecurityAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeSecurityAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        public DataAccessPictures dap = new DataAccessPictures();

        // GET api/service/date
        [HttpGet("{date}")]
        public async Task<List<Picture>> GetByDate(DateTime date)
        {
            return await dap.GetPictureByDate(date);
        }

        //POST api/service
        [HttpPost]
        public async Task<List<Picture>> RequestByDate(DateTime date1, DateTime date2)
        {
            return await dap.GetPictureByIntervallum(date1,date2);
        }
    }
}