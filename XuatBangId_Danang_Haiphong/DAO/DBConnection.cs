using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;

namespace XuatBangId_Danang_Haiphong
{
    public partial class DBConnection
    {
        static public string GetConnectionString()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string host = appSettings["host"] ?? "Not Found";
                string username = appSettings["username"] ?? "Not Found";
                string password = appSettings["password"] ?? "Not Found";
                string db = appSettings["db"] ?? "Not Found";
                return "Server=" + host + ";Database=" + db + ";Uid=" + username + ";Pwd=" + password + ";Port=3306; Charset=utf8;";
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return "error";
            }
        }

        static public DataTable GetDataByQuery(string query)
        {
            string MysqlConnectionString = GetConnectionString();
            //
            MySqlConnection con = new MySqlConnection(MysqlConnectionString);
            con.Open();
            try
            {
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand(query, con);

                DataTable table = new DataTable();
                MyDA.Fill(table);
                //
                return table;
            }
            catch (Exception ex)
            {
                //throw;
                Debug.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        static public void ExecuteQuery(string query)
        {
            string MysqlConnectionString = GetConnectionString();
            //
            MySqlConnection con = new MySqlConnection(MysqlConnectionString);
            con.Open();
            try
            {
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                MyDA.SelectCommand = new MySqlCommand(query, con);

                MyDA.SelectCommand.ExecuteNonQuery();
                //
            }

            catch (Exception)
            {
                //throw;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        static public void LoadBuck(string table_name, string file_name)
        {
            string MysqlConnectionString = GetConnectionString();
            //
            MySqlConnection con = new MySqlConnection(MysqlConnectionString);
            con.Open();
            try
            {
                var bl = new MySqlBulkLoader(con);
                bl.TableName = table_name;
                bl.FieldTerminator = "^&*";
                bl.LineTerminator = "\r\n";
                bl.FileName = file_name;
                bl.NumberOfLinesToSkip = 1;
                var inserted = bl.Load();
                Debug.Print(inserted + " rows inserted.");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message + " Error.");
            }
        }

        static public void ExecuteQueryWithParameters(string query, object[] _params)
        {
            string MysqlConnectionString = GetConnectionString();
            //
            MySqlConnection con = new MySqlConnection(MysqlConnectionString);
            con.Open();
            try
            {
                MySqlDataReader myReader = null;
                MySqlCommand myCommand = new MySqlCommand(query, con);
                for (int i = 0; i < _params.Length; i++)
                {
                    myCommand.Parameters.AddWithValue(String.Concat("@", i), _params[i]);
                }
                myReader = myCommand.ExecuteReader();
                myReader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DBConnection.ExecuteQueryWithParameters: Error when execute query: " + query + " - Exception: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        static public DataTable GetDataByQueryWithParameter(string query, object[] _params)
        {
            string MysqlConnectionString = GetConnectionString();
            //
            MySqlConnection con = new MySqlConnection(MysqlConnectionString);
            con.Open();
            try
            {
                MySqlDataAdapter adapt = null;
                MySqlCommand myCommand = new MySqlCommand(query, con);
                for (int i = 0; i < _params.Length; i++)
                {
                    myCommand.Parameters.AddWithValue(String.Concat("@", i), _params[i]);
                }

                adapt = new MySqlDataAdapter(myCommand);
                DataTable table = new DataTable();
                adapt.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DBConnection.GetDataByQueryWithParameter: Error when execute query: " + query + " - Exception: " + ex.Message);
                return null;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}