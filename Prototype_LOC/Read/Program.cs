using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using StackExchange.Redis;

namespace Read
{
    /**
     * This class is used to read data from the database (Table: latest_loc). And praint them to the Console.
     * 
     * For 5000 records, less than 1s.
     **/
    class Program
    {

        private static ConnectionMultiplexer redis;
        private static IDatabase db;
        private static ISubscriber sub;

        //private MySqlConnection connection;
        //private string server;
        //private string database;
        //private string uid;
        //private string password;
        //private string port;

        //public void initialize(string server, string database, string uid, string password, string port)
        //{
        //    server = "localhost";
        //    database = "connectcsharptomysql";
        //    uid = "username";
        //    password = "password";  
        //    this.server = server;
        //    this.uid = uid;
        //    this.password = password;
        //    this.port = port;
        //    this.database = database;
        //    string connectionstring = "data source=" + server + ";" + "port=" + port + ";" + "database=" + database + ";" + "user id=" + uid + ";" + "password=" + password + ";" + "charset = utf8";
        //    connection = new mysqlconnection(connectionstring);
        //}

        private static void Initialize(string server, string port = "6379")
        {


            //
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");
            redis = ConnectionMultiplexer.Connect(server + ":" + port);

            //
            db = redis.GetDatabase();
            sub = redis.GetSubscriber();
        }

        //public bool OpenConnection()
        //{
        //    try
        //    {
        //        connection.Open();
        //        return true;
        //    }
        //    catch (MySqlException ex)
        //    {  
        //        switch (ex.Number)
        //        {
        //            case 0:
        //                break;

        //            case 1045:
        //                break;
        //        }
        //        return false;
        //    }
        //}


        //public bool CloseConnection()
        //{
        //    try
        //    {
        //        connection.Close();
        //        return true;
        //    }
        //    catch (MySqlException ex)
        //    {
    
        //        return false;
        //    }
        //}

        //public List<Loc> Select()
        //{
        //    string query = "SELECT * FROM latest_loc";

        //    //Create a list to store the result  
        //    List<Loc> list = new List<Loc>();
 
        //    //Open connection  
        //    if (this.OpenConnection() == true)
        //    {
        //        //Create Command  
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        //Create a data reader and Execute the command  
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        //Read the data and store them in the list  
        //        while (dataReader.Read())
        //        {
        //            String user_id =(String) dataReader["user_id"];
        //            double lat = (double)dataReader["lat"];
        //            double lon = (double)dataReader["lon"];
        //            DateTime timestamp = (DateTime)dataReader["timestamp"];
        //            Loc loc = new Loc(user_id, lat,lon,timestamp);
        //            list.Add(loc);
        //        }
        //        //close Data Reader  
        //        dataReader.Close();
        //        //close Connection  
        //        this.CloseConnection();
        //        //return list to be displayed  
        //        return list;
        //    }
        //    else
        //    {
        //       return list;
        //    }
        //}



        static void Main(string[] args)
        {
            SW sw = new SW();
            Program program = new Program();
            Initialize("10.10.1.36");
            int numberOfMsg = 0;
            //while (true)
            //{
                //sw.start();
                //program.Initialize("10.10.0.93", "phoenix", "phoenix", "password", "3306");
                //List<Loc> result = program.Select();
               
                //foreach (Loc loc in result)
                //{
                //    Console.WriteLine(loc.user_id + ";" + loc.lat + ";" + loc.lon + ";" + loc.dt.ToString());
                //}
                //program.CloseConnection();
                //sw.end();
                //Console.WriteLine(sw.getTime());
                //sw.reset();
                //program.CloseConnection();
                //System.Threading.Thread.Sleep(5000);
                sub.Subscribe("userLoc", (channel, message) =>
                {
                    Console.WriteLine(message);
                    numberOfMsg++;
                    Console.WriteLine(numberOfMsg);
                });
                
            //}

            Console.ReadKey();
        }
    }
}
