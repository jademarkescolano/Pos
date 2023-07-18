using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Pos.Client.Pages;
using Pos.Server.Class;
using Pos.Shared;
using System.Data;

namespace Pos.Server.Services
{
    public class PServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public PServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Services
        public async Task<List<xservices>> Services()
        {
            List<xservices> _services = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT * FROM services", con)
                {
                    CommandType = CommandType.Text
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _services.Add(new xservices
                    {
                        serviceid = Convert.ToInt32(rdr["serviceid"]),
                        service = rdr["service"].ToString(),
                        fee = rdr["fee"].ToString(),
                    }); ;
                }
            }

            return _services;
        }


        //Add Services
        public async Task<int> AddServices(xservices _services)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("INSERT INTO services (serviceid,service,fee) VALUES " +
                                           "(@serviceid,@service,@fee)", con)
                {
                    CommandType = CommandType.Text,
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@serviceid", _services.serviceid);
                com.Parameters.AddWithValue("@service", _services.service);
                com.Parameters.AddWithValue("@fee", _services.fee);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }


        public async Task<int> UpdateServices(xservices _services)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UPDATE services SET service=@service, fee=@fee WHERE serviceid=@serviceid", con)
                {
                    CommandType = CommandType.Text,
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@serviceid", _services.serviceid);
                com.Parameters.AddWithValue("@service", _services.service);
                com.Parameters.AddWithValue("@fee", _services.fee);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }
            return i;
        }


        //Search Service
        public async Task<List<xservices>> SearchService(string search)
        {
            List<xservices> _service = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchService", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _service.Add(new xservices
                    {
                        serviceid = Convert.ToInt32(rdr["serviceid"]),
                        service = rdr["service"].ToString(),
                        fee = rdr["fee"].ToString(),

                    });
                }
            }
            return _service;
        }

        //Delete Services
        public async Task<int> DeleteServices(xservices xservices)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("DELETE FROM services WHERE serviceid = @serviceid", con)
                {
                    CommandType = CommandType.Text,
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@serviceid", xservices.serviceid);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            return i;
        }



    }
}
