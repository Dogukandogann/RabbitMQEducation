using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDesigns.P2P
{
    public static class P2pConsumer
    {
        public static void Consume()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string queueName = "example-p2p-queue";

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(queue:queueName,autoAck:false,consumer:consumer);

            consumer.Received += (sender, e) =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
            };


        }
    }
 }
