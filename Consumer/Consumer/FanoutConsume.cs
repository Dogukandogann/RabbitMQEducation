using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class FanoutConsume
    {
        public static void Publisher()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

            Console.WriteLine("Kuyruk Adı Giriniz");
            string _queueName = Console.ReadLine();
            channel.QueueDeclare(queue:_queueName,exclusive:false);

            channel.QueueBind(queue:_queueName,exchange: "fanout-exchange-example",routingKey:string.Empty);

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: _queueName, true, consumer);

            consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
            }; 
        }
    }
}
