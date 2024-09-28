using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDesigns.WorkQueue
{
    public static class WorkQueuePublisher
    {
        public static async void Publish()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string queueName = "example-work-queue";

            channel.QueueDeclare(queue:queueName,durable:false,exclusive:false,autoDelete:false);

            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(200);

                byte[] message = Encoding.UTF8.GetBytes($"Message-{i}");

                channel.BasicPublish(exchange:string.Empty,routingKey:queueName,body:message);
            }
        }
    }
}
