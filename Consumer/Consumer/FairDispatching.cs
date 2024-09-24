using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class FairDispatching
    {
        public static void FairDispatchingConsumer()
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            //Kuyruk oluşturma
            channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);
            //Fair Dispatching
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            //Mesaj okuma
            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(queue: "example-queue", true, consumer);
            consumer.Received += (sender, e) =>
            {
                //Kuyruğa gelen mesajın işlenidği yer
                //e.Body : kuyruktaki mesajın verisini bütünsel olarak getirecektir.
                //e.Body.Span() veya e.Body.ToArray() mesajın byte verisini getirecektir.
                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
            };


        }
    }
}
