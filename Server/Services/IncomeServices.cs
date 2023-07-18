using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Pos.Server.Class;
using System.Data;

namespace Pos.Server.Services
{
    public class IncomeServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public IncomeServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //Total Revenue Today
        public async Task<int> TotalIncomeToday()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("TotalIncomeToday", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }


        //Total Revenue Monthly
        public async Task<int> TotalIncomeMonthly()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("TotalIncomeMonthly", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }



    }
}
