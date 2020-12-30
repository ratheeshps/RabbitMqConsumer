using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqConsumer.Exchange_Demo
{
    public static class HeaderConsumer
    {
        public static void Receive(IModel channel)
        {
            channel.ExchangeDeclare("header-exchange", ExchangeType.Headers);
            channel.QueueDeclare("header-queue", true, false, false, arguments: null);
            var header = new Dictionary<string, object>
            {
                {"account","new" }
            };
            channel.QueueBind("header-queue", "header-exchange", string.Empty,header);
            channel.BasicQos(0, 10, false); //prefetch count to process multiple messages at a time 
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("header-queue", true, consumer);
            Console.WriteLine("Header exchange consumer started..");
        }
    }
}
