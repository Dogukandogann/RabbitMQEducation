using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class HeaderExchangeConsume
    {
        public static async void Consume()
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers);

            Console.WriteLine("Header value ?");
            string value = Console.ReadLine();

            string queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue:queueName,exchange: "header-exchange-example",routingKey:string.Empty,new Dictionary<string, object> { ["no"]= value });

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
            };
        }

     }
}
