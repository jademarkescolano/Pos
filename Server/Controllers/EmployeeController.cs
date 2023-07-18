using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos.Server.Services;
using Pos.Shared;

namespace Pos.Server.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
       EmployeeServices xservices;

        public EmployeeController(EmployeeServices xservices)
        {
            this.xservices = xservices;

        }

        //View Client
        [HttpGet]
        //[Authorize(Policy = "Admin")]
        public async Task<List<employees>> Employee()
        {
            var ret = await xservices.Employee();
            return ret;
        }


        //Add Client
        [HttpPost]
        //[Authorize]
        public async Task<int> AddEmployee([FromBody] employees _client)
        {
            var ret = await xservices.AddEmployee(_client);
            return ret;
        }

        //Update Client
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateEmployee([FromBody] employees _client)
        {
            var ret = await xservices.UpdateEmployee(_client);
            return ret;
        }


        //Search Client
        [HttpGet]
        //[Authorize]
        public async Task<List<employees>> SearchEmployee(string search)
        {
            var ret = await xservices.SearchEmployee(search);
            return ret;
        }
    }
}
