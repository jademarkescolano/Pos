using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pos.Server.Services;
using Pos.Shared;

namespace Pos.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserServices xservices;

        public UserController(UserServices xservices)
        {
            this.xservices = xservices;

        }

        //View
        [HttpGet]
        [Authorize(Policy ="Admin")]
        public new async Task<List<user>> User()
        {
            var ret = await xservices.User();
            return ret;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<user>> Login(string user, string pwd)
        {
            var ret = await xservices.Login(user, pwd);
            return ret;
        }
    }
}
