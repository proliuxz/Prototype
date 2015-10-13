using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Fleck;

namespace Prototype0._1
{
    public partial class Server : System.Web.UI.Page
    {
        public class Position
        {
            public int id;
            public double px;
            public double py;

            public Position(int id, double x, double y)
            {
                this.id = id;
                this.px = x;
                this.py = y;
            }

            public void change()
            {
                Random ran = new Random();
                int n = ran.Next();
                switch(n%9)
                {
                    case 0:
                        {
                            px = Util.AddDoubleData(px,0.001,3);
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
                    case 8:{
                        break;
                    }
                }

            }
           
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Start_Click(object sender, EventArgs e)
        {
            FleckLog.Level = LogLevel.Debug;
            List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
            WebSocketServer server = new WebSocketServer("ws://10.10.0.66:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                };
            });

            List<Position> posList = new List<Position>();
            for (int i = 0; i < 100; i++)
            {
                var pos = new Position(i, 1.306, 103.770);
                posList.Add(pos);
            }

            while (true)
            {
                foreach(var pos in posList)
                {
                    pos.change();
                    var json = new JavaScriptSerializer().Serialize(pos);
                    Console.WriteLine(json);
                    String input = json;
                    foreach (var socket in allSockets.ToList())
                    {
                        socket.Send(input);
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }


}
