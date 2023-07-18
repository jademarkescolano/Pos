using Microsoft.AspNetCore.Mvc;
using Pos.Server.Services;
using Pos.Shared;

namespace Pos.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : Controller
    {
        Services.Clients xservices;

        public ClientController(Services.Clients xservices)
        {
            this.xservices = xservices;

        }

        //View Client
        [HttpGet]
        //[Authorize]
        public async Task<List<client>> Client()
        {
            var ret = await xservices.Client();
            return ret;
        }

        
        //Add Client
        [HttpPost]
        //[Authorize]
        public async Task<int> AddClient([FromBody] client _client)
        {
            var ret = await xservices.AddClient(_client);
            return ret;
        }

        //Update Client
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateClient([FromBody] client _client)
        {
            var ret = await xservices.UpdateClient(_client);
            return ret;
        }

        //Count Client
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalClient()
        {
            return await xservices.TotalClient();
        }

       

        //Search Client
        [HttpGet]
        //[Authorize]
        public async Task<List<client>> SearchClient(string search)
        {
            var ret = await xservices.SearchClient(search);
            return ret;
        }
    }
}
