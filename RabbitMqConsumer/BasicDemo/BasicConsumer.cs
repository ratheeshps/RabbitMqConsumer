using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqConsumer.BasicDemo
{
    public static class BasicConsumer
    {
        public static void Receive(IModel channel)
        {
            channel.QueueDeclare("demo-queue", true, false, false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-queue", true, string.Empty, consumer);
        }
    }
}
