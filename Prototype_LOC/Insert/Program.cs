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
        private static ConnectionMultiplexer redis;
        private static IDatabase db;
        private static ISubscriber sub;

        static void Main(string[] args)
        {
            //int start = 0;
            int end;
            SW sw = new SW();
            //if (0 == args.Length)
            //{
            //    end = 2500;
            //}
            //else
            //{
                end = Int32.Parse(args[0]);
            //}

            
            Initialize("10.10.1.36");
            

            //while (false)
            //{
                
            //    SW sw = new SW();
            //    sw.start();

            //    try
            //    {
            //        for (int i = start; i < end; i++)
            //        {
            //            //insert into latest and historical
            //            insert("Pid" + i.ToString(), 1.030001 + i, 130.000001 + i);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //    finally
            //    {
            //        CloseDown();
            //    }
            //    sw.end();

            //    Console.WriteLine(sw.getTime());

            //}

            //sub.Subscribe("userLoc", (channel, message) => {
            //    Console.WriteLine(message);
            //});
            sw.start();
            for (int i = 0; i < end; i++)
            {
                publish("Pid"+i, 1.030001, 130.000001);
            }
            sw.end();
            Console.WriteLine(sw.getTime());
            //String value = db.StringGet("Pid");
            //Console.WriteLine(value); 
            Console.ReadLine();

        }

        private static void Initialize(string server, string port="6379")
        {
            

            //
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");
            redis = ConnectionMultiplexer.Connect(server + ":" + port);

            //
            db = redis.GetDatabase();
            sub = redis.GetSubscriber();
        }

        private static void CloseDown() {
           
        }

        private static void insert(string userid, double lat, double lon)
        {
            string value = DateTime.Now.ToString() + lat + lon;
            db.StringSet(userid, value, flags: CommandFlags.FireAndForget);
        }

        private static void publish(string userid, double lat, double lon)
        {
            string value = userid + "|" + DateTime.Now.ToString() + "|" + lat + "|" + lon;
            
            sub.Publish("userLoc", value, flags: CommandFlags.FireAndForget);
        }

    }
}
