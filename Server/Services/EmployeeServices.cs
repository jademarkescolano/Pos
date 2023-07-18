using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Pos.Server.Class;
using System.Data;
using Pos.Shared;

namespace Pos.Server.Services
{
    public class EmployeeServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;

        public EmployeeServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Client
        public async Task<List<employees>> Employee()
        {
            List<employees> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewEmployees", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new employees
                    {
                        clientid = Convert.ToInt32(rdr["empid"]),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        address = rdr["address"].ToString(),
                        contact = rdr["contact"].ToString(),

                    }); ;
                }
            }
            return _client;
        }

        //Search Client
        public async Task<List<employees>> SearchEmployee(string search)
        {
            List<employees> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new employees
                    {
                        clientid = Convert.ToInt32(rdr["empid"]),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        address = rdr["address"].ToString(),
                        contact = rdr["contact"].ToString(),
                        lname = rdr["lname"].ToString(),
                        fullname = rdr["fullname"].ToString(),

                    });
                }
            }
            return _client;
        }



        //Add Client
        public async Task<int> AddEmployee(employees _client)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_empid", _client.clientid);
                com.Parameters.AddWithValue("_fname", _client.fname);
                com.Parameters.AddWithValue("_mname", _client.mname);
                com.Parameters.AddWithValue("_lname", _client.lname);
                com.Parameters.AddWithValue("_address", _client.address);
                com.Parameters.AddWithValue("_contact", _client.contact);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }

        //Update Client
        public async Task<int> UpdateEmployee(employees _client)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateEmployee", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_empid", _client.clientid);
                com.Parameters.AddWithValue("_fname", _client.fname);
                com.Parameters.AddWithValue("_mname", _client.mname);
                com.Parameters.AddWithValue("_lname", _client.lname);
                com.Parameters.AddWithValue("_address", _client.address);
                com.Parameters.AddWithValue("_contact", _client.contact);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
            return i;
        }

    }
}

