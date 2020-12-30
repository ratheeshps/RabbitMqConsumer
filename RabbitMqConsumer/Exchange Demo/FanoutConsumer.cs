using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqConsumer.Exchange_Demo
{
    public static class FanoutConsumer
    {
        public static void Receive(IModel channel)
        {
            channel.ExchangeDeclare("fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("fanout-queue", true, false, false, arguments: null);
            var header = new Dictionary<string, object>
            {
                {"account","new" }
            };
            channel.QueueBind("fanout-queue", "fanout-exchange", string.Empty, header);

            channel.BasicQos(0, 10, false); //prefetch count to process multiple messages at a time 

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("fanout-queue", true, consumer);
            Console.WriteLine("Fanout exchange consumer started..");
        }
    }
}
