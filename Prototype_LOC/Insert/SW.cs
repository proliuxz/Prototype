using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Insert
{
    public class SW
    {
        private Stopwatch sw;
        public SW()
        {
            sw = new Stopwatch();
        }
        public void start()
        {
            sw.Start();
        }
        public void end()
        {
            sw.Stop();
        }
        public decimal getTime()
        {
            return sw.ElapsedMilliseconds;
        }
        public void reset()
        {
            sw.Reset();
        }

    }
}
