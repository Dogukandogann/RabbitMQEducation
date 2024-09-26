using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class TopicExchangeConsume
    {
        public static async void Consume()
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topix-exchange-example", type: ExchangeType.Topic);

            Console.WriteLine("Dinlenecek Topic?");
            string topic = Console.ReadLine();

            string queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName,exchange: "topix-exchange-example",routingKey:topic);

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue:queueName,autoAck:true,consumer:consumer);
            consumer.Received +=(sender,e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
            };
        }
    }
}
