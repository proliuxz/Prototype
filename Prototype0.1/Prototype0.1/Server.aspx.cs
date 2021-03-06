﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fleck;

namespace Prototype0._1
{
    public partial class Server : System.Web.UI.Page
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Start_Click(object sender, EventArgs e)
        {
            FleckLog.Level = LogLevel.Debug;
            List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
            WebSocketServer server = new WebSocketServer("ws://10.10.0.239:8181");
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

            while (true)
            {
                String input = DateTime.Now.ToString();
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                Thread.Sleep(1000);
            }
        }
    }
}
