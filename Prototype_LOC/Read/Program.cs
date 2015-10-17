using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;

namespace Read
{
    /**
     * This class is used to read data from the database (Table: latest_loc). And praint them to the Console.
     * 
     * For 5000 records, less than 1s.
     **/
    class Program
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;

        public void Initialize(string server, string database, string uid, string password, string port)
        {
            //server = "localhost";  
            //database = "connectcsharptomysql";  
            //uid = "username";  
            //password = "password";  
            this.server = server;
            this.uid = uid;
            this.password = password;
            this.port = port;
            this.database = database;
            string connectionString = "Data Source=" + server + ";" + "port=" + port + ";" + "Database=" + database + ";" + "User Id=" + uid + ";" + "Password=" + password + ";" + "CharSet = utf8";
            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {  
                switch (ex.Number)
                {
                    case 0:
                        break;

                    case 1045:
                        break;
                }
                return false;
            }
        }


        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
    
                return false;
            }
        }

        public List<Loc> Select()
        {
            string query = "SELECT * FROM latest_loc";

            //Create a list to store the result  
            List<Loc> list = new List<Loc>();
 
            //Open connection  
            if (this.OpenConnection() == true)
            {
                //Create Command  
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command  
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list  
                while (dataReader.Read())
                {
                    String user_id =(String) dataReader["user_id"];
                    double lat = (double)dataReader["lat"];
                    double lon = (double)dataReader["lon"];
                    DateTime timestamp = (DateTime)dataReader["timestamp"];
                    Loc loc = new Loc(user_id, lat,lon,timestamp);
                    list.Add(loc);
                }
                //close Data Reader  
                dataReader.Close();
                //close Connection  
                this.CloseConnection();
                //return list to be displayed  
                return list;
            }
            else
            {
               return list;
            }
        }

        public List<Loc> SelectByCondition(int begin,int end)
        {
            string query = "SELECT * FROM latest_loc where user_id>" + begin + "&&user_id<"+end;

            //Create a list to store the result  
            List<Loc> list = new List<Loc>();

            //Open connection  
            if (this.OpenConnection() == true)
            {
                //Create Command  
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command  
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list  
                while (dataReader.Read())
                {
                    String user_id = (String)dataReader["user_id"];
                    double lat = (double)dataReader["lat"];
                    double lon = (double)dataReader["lon"];
                    DateTime timestamp = (DateTime)dataReader["timestamp"];
                    Loc loc = new Loc(user_id, lat, lon, timestamp);
                    list.Add(loc);
                }
                //close Data Reader  
                dataReader.Close();
                //close Connection  
                this.CloseConnection();
                //return list to be displayed  
                return list;
            }
            else
            {
                return list;
            }
        }


        static void Main(string[] args)
        {
            Program program = new Program();
            program.Initialize("10.10.0.93", "phoenix", "phoenix", "password", "3306");
            while (true)
            {
                
                List<Loc> result = program.SelectByCondition(0, 0);
                Console.WriteLine(DateTime.Now.ToString());
                foreach (Loc loc in result)
                {
                    Console.WriteLine(loc.user_id + ";" + loc.lat + ";" + loc.lon + ";" + loc.dt.ToString());
                }
                Console.WriteLine(DateTime.Now.ToString());
                program.CloseConnection();
                System.Threading.Thread.Sleep(5000);
            }
            
        }
    }
}
