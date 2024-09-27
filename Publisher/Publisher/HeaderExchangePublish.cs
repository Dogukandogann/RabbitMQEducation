using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    public static class HeaderExchangePublish
    {
        public static async void Publish()
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange:"header-exchange-example",type:ExchangeType.Headers);

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(200);

                byte[] message = Encoding.UTF8.GetBytes($"Hello World {i}");

                Console.Write("Header value ?");
                string value = Console.ReadLine();

                IBasicProperties properties =  channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>()
                {
                    ["no"] = value,
                };

                channel.BasicPublish(exchange: "header-exchange-example",routingKey:string.Empty,body:message,basicProperties:properties);
            }

        }
    }
}
