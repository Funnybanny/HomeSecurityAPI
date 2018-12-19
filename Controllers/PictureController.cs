using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSecurityAPI.DataAccess;
using HomeSecurityAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HomeSecurityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        public DataAccessPictures dap = new DataAccessPictures(); 

        // GET api/picture/id
        [HttpGet]
        public async Task<List<Picture>> GetAll(int id)
        {
           return  await dap.GetAllbyUserID(id);
        }
        // POST api/picture
        [HttpPost]
        public async void Post([FromBody] Picture p)
        {
            await dap.Create(p);
        }

        // DELETE api/picture/objId
        [HttpDelete]
        public async void Delete(string objId)
        {
            await dap.Delete(objId);
        }
    }
}