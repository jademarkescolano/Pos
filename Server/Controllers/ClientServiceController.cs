using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Pos.Server.Class;
using Pos.Server.Services;
using Pos.Shared;
using System.Data;

namespace Pos.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientServiceController : Controller
    {
        ClientServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClientServiceController(ClientServices xservices, IWebHostEnvironment webHostEnvironment)
        {
            this.xservices = xservices;
            _webHostEnvironment = webHostEnvironment;
        }

        //View Client Services
        [HttpGet]
        //[Authorize]
        public async Task<List<clientservices>> ClientService()
        {
            var ret = await xservices.ClientService();
            return ret;
        }

        //View Client Services Today
        [HttpGet]
        //[Authorize]
        public async Task<List<clientservices>> ClientServiceToday()
        {
            var ret = await xservices.ClientServiceToday();
            return ret;
        }


        //Search Client Services
        [HttpGet]
        //[Authorize]
        public async Task<List<clientservices>> SearchClientService(string search)
        {
            var ret = await xservices.SearchClientService(search);
            return ret;
        }


        //Add Client Services
        [HttpPost]
        //[Authorize]
        public async Task<int> AddClientService([FromBody] clientservices _client)
        {
            var ret = await xservices.AddClientService(_client);
            return ret;
        }

        //Update Client Services
        [HttpPut]
        //[Authorize]
        public async Task<int> UpdateClientService([FromBody] clientservices _client)
        {
            var ret = await xservices.UpdateClientService(_client);
            return ret;
        }

        //Count Today Client
        [HttpGet]
        //[Authorize]
        public async Task<int> CountToday()
        {
            return await xservices.CountToday();
        }

        //Search ClientService Bydate
        [HttpGet]
        //[Authorize]
        public async Task<List<clientservices>> SearchDateClientService(DateTime start, DateTime end)
        {
            var ret = await xservices.SearchDateClientService(start, end);
            return ret;
        }

        //Client Report By Date
        [HttpGet]
        public async Task<IActionResult> ClientServiceReport(DateTime start, DateTime end)
        {
            ListtoTable listtoTable = new();
            var dt = new DataTable();
            var lst = await xservices.SearchDateClientService(start, end);
            dt = listtoTable.ToDataTablee(lst);
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "ClientReport.rdlc");
            Stream reportDefinition;

            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            byte[] excel = report.Render("EXCEL");
            fs.Dispose();

            return File(excel, "application/msexcel", "ClientService.xls");
        }


    }
}
