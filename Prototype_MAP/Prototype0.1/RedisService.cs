using Fleck;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype0._1
{
    public class RedisService
    {
        private static ConnectionMultiplexer redis;
        private static IDatabase db;
        private static ISubscriber sub;
        public static void Initialize(string server, string port = "6379")
        {

            redis = ConnectionMultiplexer.Connect(server + ":" + port);

            db = redis.GetDatabase();
            sub = redis.GetSubscriber();
        }

        public static void Subscribe(ReceiveMessageDelegate callback)
        {
            //Initialize("127.0.0.1");
            
            sub.Subscribe("userLoc", (channel, message) =>
            {
                callback(message.ToString());
            });
        }


        public static void publish(string message)
        {
            sub.Publish("userLoc", message, flags: CommandFlags.FireAndForget);
            //sub.Publish("userLoc", value);
        }

    }
}