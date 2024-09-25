using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class DirectExchangeConsume
    {
        public static void Publisher()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            //Publisherda olan exchange ile birebir aynı isim ve type olmalıdır.
            channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

            //Publisher tarafında routingKey'de bulunan değerdeki kuyruğa gönderilen mesajları kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir.
            string queueName = channel.QueueDeclare().QueueName;

            //routingKey ile kuyruğu bağlama işlemi.
            channel.QueueBind(queue: queueName,exchange: "direct-exchange-example",routingKey: "direct-queue-example");
            
            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: queueName, true, consumer);
            consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
            };
        }
    }
}
