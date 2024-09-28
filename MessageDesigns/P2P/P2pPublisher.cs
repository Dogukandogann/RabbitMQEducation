using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDesigns.P2P
{
    public static class P2pPublisher
    {
        public static void Publish()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string queueName = "example-p2p-queue";

            channel.QueueDeclare(queue:queueName,durable:false,exclusive:false,autoDelete:false);

            byte[] message = Encoding.UTF8.GetBytes("Hello, World!");

            channel.BasicPublish(exchange:"",routingKey:queueName,body:message);
        }
    } 
}
