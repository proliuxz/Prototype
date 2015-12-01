using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Fleck;
using StackExchange.Redis;

namespace Prototype0._1
{
    public partial class Server : System.Web.UI.Page
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private List<Position> posList = new List<Position>();
        private List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Start_Click(object sender, EventArgs e)
        {
            FleckLog.Level = LogLevel.Debug;
            
            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:8181");
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

            ReceiveMessageDelegate cb = ReceiveMessageCallback;

            RedisService.Initialize("127.0.0.1");
            RedisService.Subscribe(cb);
            definePostitionList(80);

            while (true)
            {
                foreach(var pos in posList)
                {
                    pos.change();
                    var json = serializer.Serialize(pos);
                    Console.WriteLine(json);
                    String input = json;
                    foreach (var socket in allSockets.ToList())
                    {
                        //socket.Send(input);
                        RedisService.publish(input);
                    }
                    
                }
                Thread.Sleep(3000);
            }
        }
        private void definePostitionList(int size) 
        {
            for (int i = 0; i < size; i++)
            {
                var pos = new Position(i, 1.306, 103.770);
                if (i%3==0)
                {
                    pos.setOccupy("Staff");
                }
                else if (i % 3 == 1)
                {
                    pos.setOccupy("Survival");
                }
                else
                {
                    pos.setOccupy("Zombie");
                }

                posList.Add(pos);
            }
        }

        

        public void ReceiveMessageCallback(String msg)
        {
            foreach (var socket in allSockets.ToList())
            {
                socket.Send(msg);
            }
        }

        

    }

    public delegate void ReceiveMessageDelegate(String msg);

}
