using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype0._1
{
    public class Position
    {
        public int id;
        public double px;
        public double py;
        public string role;

        public Position(int id, double x, double y)
        {
            this.id = id;
            this.px = x;
            this.py = y;
        }

        public void setOccupy(string r)
        {
            this.role = r;
        }

        public void change()
        {
            Random ran = new Random();
            int n = ran.Next();
            switch (n % 9)
            {
                case 0:
                    {
                        px = Util.AddDoubleData(px, 0.001, 3);
                        py = Util.AddDoubleData(py, 0.001, 3);
                        break;
                    }
                case 1:
                    {
                        px = Util.AddDoubleData(px, 0.001, 3);
                        py = Util.MinDoubleData(py, 0.001, 3);
                        break;
                    }

                case 2:
                    {
                        px = Util.MinDoubleData(px, 0.001, 3);
                        py = Util.AddDoubleData(py, 0.001, 3);
                        break;
                    }
                case 3:
                    {
                        px = Util.MinDoubleData(px, 0.001, 3);
                        py = Util.MinDoubleData(py, 0.001, 3);
                        break;
                    }
                case 4:
                    {
                        px = Util.AddDoubleData(px, 0.001, 3);
                        break;
                    }
                case 5:
                    {
                        py = Util.AddDoubleData(py, 0.001, 3);
                        break;
                    }

                case 6:
                    {
                        px = Util.MinDoubleData(px, 0.001, 3);
                        break;
                    }
                case 7:
                    {
                        py = Util.MinDoubleData(py, 0.001, 3);
                        break;
                    }
                case 8:
                    {
                        break;
                    }
            }

        }

    }

}