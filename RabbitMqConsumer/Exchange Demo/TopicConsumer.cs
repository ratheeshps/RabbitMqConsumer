using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqConsumer.Exchange_Demo
{
    public static class TopicConsumer
    {
        public static void Receive(IModel channel)
        {
            channel.ExchangeDeclare("topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare("topic-queue", true, false, false, arguments: null);
            channel.QueueBind("topic-queue", "topic-exchange", "payment.*");

            channel.BasicQos(0, 10, false); //prefetch count to process multiple messages at a time 

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("topic-queue", true, consumer);
            Console.WriteLine("Topic exchange consumer started..");
        }
    }
}
