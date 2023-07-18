using Microsoft.AspNetCore.Mvc;
using Pos.Server.Services;

namespace Pos.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncomeController : Controller
    {
        IncomeServices xservices;

        public IncomeController(IncomeServices xservices)
        {
            this.xservices = xservices;

        }

        //Today Total Revenue
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalIncomeToday()
        {
            return await xservices.TotalIncomeToday();
        }

        //Monthly Total Revenue
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalIncomeMonthly()
        {
            return await xservices.TotalIncomeMonthly();
        }
    }
}
