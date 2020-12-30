using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqConsumer.BasicDemo;
using RabbitMqConsumer.Exchange_Demo;
using System;
using System.Text;

namespace RabbitMqConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Howdy, I am a RabbitMQ Consumer");

            #region "Connection and initialization"
            string UserName = "guest";
            string Password = "guest";
            string HostName = "localhost";
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = UserName,
                Password = Password,
                HostName = HostName
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel(); //Create a channel
            #endregion

            #region "Basic Demo"
            // BasicConsumer.Receive(channel);
            #endregion
            #region "Direct exhange demo"
            //DirectExchangeConsumer.Receive(channel);
            #endregion

            #region "Topic exhange demo"
            //TopicConsumer.Receive(channel);
            #endregion

            #region "Header exhange demo"
           // HeaderConsumer.Receive(channel);
            #endregion

            #region "Fanout exhange demo"
            FanoutConsumer.Receive(channel);
            #endregion

            Console.ReadLine();
        }
    }
}
