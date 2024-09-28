using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDesigns.PublishSubscribe
{
    public static class PublishSubPublisher
    {
        public static void Publish()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string exchangeName = "example-ps-queue";

            channel.ExchangeDeclare(exchange:exchangeName,type:ExchangeType.Fanout);

            byte[] message = Encoding.UTF8.GetBytes("Hello World!");

            channel.BasicPublish(exchange:exchangeName,routingKey:string.Empty,body:message);
        }
    }
}
