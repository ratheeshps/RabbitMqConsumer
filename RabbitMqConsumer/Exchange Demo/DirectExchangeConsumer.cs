using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqConsumer.Exchange_Demo
{
   public static class DirectExchangeConsumer
    {
            public static void Receive(IModel channel)
            {
                channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);
                channel.QueueDeclare("direct-queue", true, false, false, arguments: null);
                channel.QueueBind("direct-queue", "direct-exchange", "payment.init");

                channel.BasicQos(0, 10, false); //prefetch count to process multiple messages at a time 

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                };
                channel.BasicConsume("direct-queue", true,  consumer);
                 Console.WriteLine("Direct consumer started..");
        }
    }
}
