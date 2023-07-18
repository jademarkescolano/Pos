using Microsoft.AspNetCore.Mvc;
using Pos.Client.Pages;
using Pos.Server.Services;
using Pos.Shared;

namespace Pos.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicesController : Controller
    {
        PServices xservices;

        public ServicesController(PServices xservices)
        {
            this.xservices = xservices;

        }

        //View Service
        [HttpGet]
        //[Authorize]
        public async Task<List<xservices>> Services()
        {
            var ret = await xservices.Services();
            return ret;
        }

        //Add Services
        [HttpPost]
        //[Authorize]
        public async Task<int> AddServices([FromBody] xservices _services)
        {
            var ret = await xservices.AddServices(_services);
            return ret;
        }

        //Update Services
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateServices([FromBody] xservices _services)
        {
            var ret = await xservices.UpdateServices(_services);
            return ret;
        }

        //Search Service
        [HttpGet]
        //[Authorize]
        public async Task<List<xservices>> SearchService(string search)
        {
            var ret = await xservices.SearchService(search);
            return ret;
        }

        //Delete Services
        [HttpPost]
        //[Authorize]
        public async Task<int> DeleteServices([FromBody] xservices _services)
        {
            var ret = await xservices.DeleteServices(_services);
            return ret;
        }
    }
}
