using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read
{
    /**
     * Entity Class for Read
     * 
     * **/
    public class Loc
    {
        public string user_id{set;get;}
        public double lat{set;get;}
        public double lon{set;get;}
        public DateTime dt{set;get;}

        public Loc(String user_id, double lat, double lon, DateTime dateTime)
        {
            this.user_id = user_id;
            this.lat = lat;
            this.lon = lon;
            this.dt = dateTime;
        }
    }
}
