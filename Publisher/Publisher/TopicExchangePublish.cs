using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    public static class TopicExchangePublish
    {
        public static async void Publish()
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topix-exchange-example", type: ExchangeType.Topic);

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(200);
                byte[] message = Encoding.UTF8.GetBytes($"Hello World! {i}");
                Console.WriteLine("Topic Belirtiniz");
                string topic = Console.ReadLine();
                channel.BasicPublish(exchange: "topix-exchange-example",routingKey: topic,body:message);
            }
        }
    }
}
