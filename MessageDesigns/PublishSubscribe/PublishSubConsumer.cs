using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDesigns.PublishSubscribe
{
    public static class PublishSubConsumer
    {
        public static void Consume()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string exchangeName = "example-ps-queue";

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

            string queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue:queueName,exchange:exchangeName,routingKey:string.Empty);

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: queueName, autoAck:true,consumer:consumer);

            consumer.Received += (sender, e) =>
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
            };
        }
    }
}
