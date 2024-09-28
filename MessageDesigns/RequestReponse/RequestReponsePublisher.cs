using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageDesigns.RequestReponse
{
    public static class RequestReponsePublisher
    {
        public static  void Publish()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");


            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string requestQueueName = "example-request-response-queue";
            channel.QueueDeclare(queue:requestQueueName,durable:false,exclusive:false,autoDelete:false);

            string replyQueueName = channel.QueueDeclare().QueueName;

            string coreelationId = Guid.NewGuid().ToString();

            //Request mesajını oluşturma ve gönderme
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.CorrelationId = coreelationId;
            properties.ReplyTo = replyQueueName;

            for (int i = 0; i < 10; i++)
            {
                byte[] messsage = Encoding.UTF8.GetBytes($"Merhaba {1}");
                channel.BasicPublish(exchange: string.Empty, routingKey: requestQueueName, basicProperties: properties, body: messsage);
            }

            //response kuyruğunu dinleme

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: replyQueueName, true, consumer);

            consumer.Received += (sender, e) =>
            {
                if (e.BasicProperties.CorrelationId == coreelationId)
                {
                    Console.WriteLine($"Reponse : {Encoding.UTF8.GetString(e.Body.Span)}");
                }
            };


        }
    }
}
