using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Pos.Server.Class;
using Pos.Shared;
using System.Data;

namespace Pos.Server.Services
{
    public class ClientServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public ClientServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }


        //View Client Services
        public async Task<List<clientservices>> ClientService()
        {
            List<clientservices> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewClientService", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new clientservices
                    {
                        clientserviceid = Convert.ToInt32(rdr["clientserviceid"]),
                        clientid = Convert.ToInt32(rdr["clientid"]),
                        name = rdr["name"].ToString(),
                        empid = Convert.ToInt32(rdr["empid"]),
                        employee = rdr["employee"].ToString(),
						receiptno = rdr["receiptno"].ToString(),
						service = rdr["service"].ToString(),
                        fee = rdr["fee"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                    }); ;
                }
            }
            return _client;
        }


        //View Client Receipt
        public async Task<List<clientservices>> ClienReceipt()
        {
            List<clientservices> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ClientReceipt", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new clientservices
                    {
                        clientserviceid = Convert.ToInt32(rdr["clientserviceid"]),
                        clientid = Convert.ToInt32(rdr["clientid"]),
                        name = rdr["name"].ToString(),
                        empid = Convert.ToInt32(rdr["empid"]),
                        employee = rdr["employee"].ToString(),
                        receiptno = rdr["receiptno"].ToString(),
                        service = rdr["service"].ToString(),
                        fee = rdr["fee"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                    }); ;
                }
            }
            return _client;
        }


        //View Client Services
        public async Task<List<clientservices>> ClientServiceToday()
        {
            List<clientservices> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewClientServiceToday", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new clientservices
                    {
                        clientserviceid = Convert.ToInt32(rdr["clientserviceid"]),
                        clientid = Convert.ToInt32(rdr["clientid"]),
                        name = rdr["name"].ToString(),
                        service = rdr["service"].ToString(),
                        empid = Convert.ToInt32(rdr["empid"]),
						receiptno = rdr["receiptno"].ToString(),
						employee = rdr["employee"].ToString(),
                        fee = rdr["fee"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                    }); ;
                }
            }
            return _client;
        }



        //Add Client Services
        public async Task<int> AddClientService(clientservices _client)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertClientService", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_clientserviceid", _client.clientserviceid);
                com.Parameters.AddWithValue("_clientid", _client.clientid);
                com.Parameters.AddWithValue("_empid", _client.empid);
                com.Parameters.AddWithValue("_service", _client.service);
                com.Parameters.AddWithValue("_fee", _client._fee);
                com.Parameters.AddWithValue("_date", _client.date);
                com.Parameters.AddWithValue("_receiptno", _client.receiptno);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }


        //Update Client Services
        public async Task<int> UpdateClientService(clientservices _client)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateClientService", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_clientserviceid", _client.clientserviceid);
                com.Parameters.AddWithValue("_clientid", _client.clientid);
              
                com.Parameters.AddWithValue("_empid", _client.empid);
                com.Parameters.AddWithValue("_receiptno", _client.receiptno);
               
                com.Parameters.AddWithValue("_service", _client.service);
                com.Parameters.AddWithValue("_fee", _client._fee);
                com.Parameters.AddWithValue("_date", _client.date);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }

        //Search Client Services
        public async Task<List<clientservices>> SearchClientService(string search)
        {
            List<clientservices> _client = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchClientService", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _client.Add(new clientservices
                    {
                        clientserviceid = Convert.ToInt32(rdr["clientserviceid"]),
                        clientid = Convert.ToInt32(rdr["clientid"]),
                        name = rdr["name"].ToString(),
                        service = rdr["service"].ToString(),
                        empid = Convert.ToInt32(rdr["empid"]),
                        receiptno = rdr["receiptno"].ToString(),
                        employee = rdr["employee"].ToString(),
                        fee = rdr["fee"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                    });
                }
            }
            return _client;
        }

        //Count Today Client
        public async Task<int> CountToday()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM clientservice WHERE DATE(date)  = CURDATE()", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }



        //Search Client Service Bydate
        public async Task<List<clientservices>> SearchDateClientService(DateTime start, DateTime end)
        {
            List<clientservices> xclientservices = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("ReportClientService", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("start_date", start);
                    com.Parameters.AddWithValue("end_date", end);

                    using (var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await rdr.ReadAsync().ConfigureAwait(false))
                        {
                            xclientservices.Add(new clientservices
                            {
                                clientserviceid = Convert.ToInt32(rdr["clientserviceid"]),
                                clientid = Convert.ToInt32(rdr["clientid"]),
                                name = rdr["name"].ToString(),
                                empid = Convert.ToInt32(rdr["empid"]),
								receiptno = rdr["receiptno"].ToString(),
								employee = rdr["employee"].ToString(),
                                service = rdr["service"].ToString(),
                                fee = rdr["fee"].ToString(),
                                date = Convert.ToDateTime(rdr["date"]),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return xclientservices;
        }

        //Report Client Service Bydate
        public async Task<List<clientservices>> ClientServiceReport(DateTime start, DateTime end)
        {
            List<clientservices> xclientservices = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("ReportClientService", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("start_date", start);
                    com.Parameters.AddWithValue("end_date", end);

                    using (var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await rdr.ReadAsync().ConfigureAwait(false))
                        {
                            xclientservices.Add(new clientservices
                            {
                                clientserviceid = Convert.ToInt32(rdr["clientserviceid"]),
                                clientid = Convert.ToInt32(rdr["clientid"]),
                                name = rdr["name"].ToString(),
                                empid = Convert.ToInt32(rdr["empid"]),
                                employee = rdr["employee"].ToString(),
								receiptno = rdr["receiptno"].ToString(),
								service = rdr["service"].ToString(),
                                fee = rdr["fee"].ToString(),
                                date = Convert.ToDateTime(rdr["date"]),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return xclientservices;
        }



    }
}
