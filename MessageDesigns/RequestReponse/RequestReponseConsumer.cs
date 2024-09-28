using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace MessageDesigns.RequestReponse
{
    public static class RequestReponseConsumer
    {
        public static void Consume()
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string requestQueueName = "example-request-response-queue";
            channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue:requestQueueName,autoAck:true,consumer:consumer);

            consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
                byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem Tamamlandı. {message}");
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.CorrelationId = e.BasicProperties.CorrelationId;
                channel.BasicPublish(exchange:string.Empty,routingKey:e.BasicProperties.ReplyTo,basicProperties:properties,body:responseMessage);
            };

        }
    }
}
