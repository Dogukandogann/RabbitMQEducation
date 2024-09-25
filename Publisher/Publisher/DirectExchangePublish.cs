using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    public static class DirectExchangePublish
    {
        public static void Publisher()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            //Exchange oluşturma
            channel.ExchangeDeclare(exchange:"direct-exchange-example",type:ExchangeType.Direct);

            //Kuyruk oluşturma
            while (true)
            {
                Console.Write("Mesaj :");
                string message = Console.ReadLine();
                byte[] byteMessage = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "direct-exchange-example",routingKey:"direct-queue-example",body:byteMessage);
            }
        }

    }
}
