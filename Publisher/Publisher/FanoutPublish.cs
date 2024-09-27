using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    public static class FanoutPublish
    {
        public static async void Publish()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange:"fanout-exchange-example",type:ExchangeType.Fanout);

            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(200);
                byte[] message = Encoding.UTF8.GetBytes($"Hello RabbitMQ : {i}");
                channel.BasicPublish(exchange: "fanout-exchange-example",routingKey:string.Empty,body:message);
            }
        } 
    }
}
