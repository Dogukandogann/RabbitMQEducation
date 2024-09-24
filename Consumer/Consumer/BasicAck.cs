using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class BasicAck
    {
        public static void BasicAckConsume()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            //Kuyruk oluşturma
            channel.QueueDeclare(queue: "example-queue", exclusive: false);

            //Mesaj okuma
            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: "example-queue", autoAck:false, consumer);
            consumer.Received += (sender, e) =>
            {
                //Kuyruğa gelen mesajın işlenidği yer
                //e.Body : kuyruktaki mesajın verisini bütünsel olarak getirecektir.
                //e.Body.Span() veya e.Body.ToArray() mesajın byte verisini getirecektir.
                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
                channel.BasicAck(deliveryTag:e.DeliveryTag,multiple: false);
            };
        }
    }
}
