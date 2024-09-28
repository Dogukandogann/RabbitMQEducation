using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDesigns.WorkQueue
{
    public static class WorkQueueConsumer
    {
        public static void Consume()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string queueName = "example-work-queue";

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);

            channel.BasicQos(prefetchCount:1,prefetchSize:0,global:false);

            consumer.Received += (sender, e) =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
            };
        }
    }
}
