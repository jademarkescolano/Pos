using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Pos.Server.Class;
using System.Data;
using Pos.Shared;

namespace Pos.Server.Services
{
    public class Clients
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;

        public Clients(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Client
        public async Task<List<client>> Client()
        {
            List<client> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewClient", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new client
                    {
                        clientid = Convert.ToInt32(rdr["clientid"]),
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
        public async Task<List<client>> SearchClient(string search)
        {
            List<client> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchClient", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new client
                    {
                        clientid = Convert.ToInt32(rdr["clientid"]),
                        fname = rdr["fname"].ToString(),
                        address = rdr["address"].ToString(),
                        contact = rdr["contact"].ToString(),
                       
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                      
                    }); 
                }
            }
            return _client;
        }



        //Add Client
        public async Task<int> AddClient(client _client)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertClient", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("p_clientid", _client.clientid);
                com.Parameters.AddWithValue("p_fname", _client.fname);
                com.Parameters.AddWithValue("p_mname", _client.mname);
                com.Parameters.AddWithValue("p_lname", _client.lname);
                com.Parameters.AddWithValue("p_address", _client.address);
                com.Parameters.AddWithValue("p_contact", _client.contact);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }

        //Update Client
        public async Task<int> UpdateClient(client _client)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateClient", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("p_clientid", _client.clientid);
                com.Parameters.AddWithValue("p_fname", _client.fname);
                com.Parameters.AddWithValue("p_mname", _client.mname);
                com.Parameters.AddWithValue("p_lname", _client.lname);
                com.Parameters.AddWithValue("p_address", _client.address);
                com.Parameters.AddWithValue("p_contact", _client.contact);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
            return i;
        }

        //Count Client
        public async Task<int> TotalClient()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM client", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }
        }
    }

