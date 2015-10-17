using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insert
{
    /**
     *   This class will keep insert data into latest_loc, and historical_loc table
     *   to emulate the web service that receive geo location data mobile app uploaded.
     */
    class Program
    {
        private static MySqlConnection connection;
        static string ins_latest = 
            " INSERT INTO `latest_loc` " +
            " (`user_id`, " +
            " `lat`, " +
            " `lon`) " + 
            " VALUES " +
            " (@ID,@LAT,@LON) " +
            " ON DUPLICATE KEY UPDATE  " +
            " `lat` = @LAT, " +
            " `lon` = @LON ";

        static string ins_historical =
            " INSERT INTO `historical_loc` " +
            " (`user_id`, " +
            " `timestamp`, " +
            " `lat`, " +
            " `lon`) " +
            " VALUES " +
            " (@ID,CURRENT_TIMESTAMP,@LAT,@LON) ";


        static void Main(string[] args)
        {
            int start = 0;
            int end = Int32.Parse(args[0]);
//            if (0 == args.Length)
//            {
//                start = 0;
//            }
//            else
//            {
//                start = Int32.Parse(args[0]);
//            }
            while (true)
            {
                //Console.WriteLine(DateTime.Now);
                SW sw = new SW();
                sw.start();
//                int end = start + 2500;

                try
                {
                   
                    Initialize("10.10.0.93", "phoenix", "phoenix", "password", "3306");
                    
                    for (int i = start; i < end; i++)
                    {

                        //insert into latest and historical
                        insert("Pid" + i.ToString(), 1.030001 + i, 130.000001 + i);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    CloseDown();
                }
                sw.end();

                Console.WriteLine(sw.getTime());

                //Console.WriteLine(DateTime.Now);
            }
            Console.ReadLine();

        }

        private static void Initialize(string server, string database, string uid, string password, string port)
        {
            string connectionString = "Data Source=" + server + ";" + "port=" + port + ";" + "Database=" + database + ";" + "User Id=" + uid + ";" + "Password=" + password + ";" + "CharSet = utf8"; ;
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        private static void CloseDown() {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }

        private static void insert(string userid, double lat, double lon)
        {
            // latest
            MySqlCommand command = new MySqlCommand(ins_latest, connection);
            //command.("@ID", SqlDbType.VarChar);
            command.Parameters.AddWithValue("@ID", userid);
            command.Parameters.AddWithValue("@LAT", lat);
            command.Parameters.AddWithValue("@LON", lon);

            try
            {
                Int32 rowsAffected = command.ExecuteNonQuery();
                //Console.WriteLine("RowsAffected: {0}", rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            //// historical
            //command = new MySqlCommand(ins_historical, connection);
            //command.Parameters.AddWithValue("@ID", userid);
            //command.Parameters.AddWithValue("@LAT", lat);
            //command.Parameters.AddWithValue("@LON", lon);

            //try
            //{
            //    Int32 rowsAffected = command.ExecuteNonQuery();
            //    //Console.WriteLine("RowsAffected: {0}", rowsAffected);
            //}
            //catch (Exception ex)
            //{
                
            //    throw ex;
            //}

        }

    }
}
