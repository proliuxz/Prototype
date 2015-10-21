//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace Insert
{
    /**
     *   This class will keep insert data into latest_loc, and historical_loc table
     *   to emulate the web service that receive geo location data mobile app uploaded.
     */
    class Program
    {

        private static IDatabase db;
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
            int end;
            if (0 == args.Length)
            {
                end = 1;
            }
            else
            {
                end = Int32.Parse(args[0]);
            }

            Initialize("10.10.0.93", "phoenix", "phoenix", "password", "3306");

            insert("Pid", 1.030001 , 130.000001);

            while (false)
            {
                
                SW sw = new SW();
                sw.start();

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

            }

            String value = db.StringGet("Pid");
            Console.WriteLine(value); 
            Console.ReadLine();

        }

        private static void Initialize(string server, string database, string uid, string password, string port)
        {
            

            //
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            //
            db = redis.GetDatabase();
        }

        private static void CloseDown() {
           
        }

        private static void insert(string userid, double lat, double lon)
        {
            string value = DateTime.Now.ToString() + lat + lon;
            db.StringSet(userid, value, flags: CommandFlags.FireAndForget);

            

        }

    }
}
