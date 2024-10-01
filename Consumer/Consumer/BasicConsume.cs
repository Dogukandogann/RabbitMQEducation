using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public static class BasicConsume
    {
        public static void BasicConsumer() 
        {
            //RabbitMQ sunucusuna bağlanmak için ConnectionFactory sınıfını kullanıyoruz.
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştiriyoruz.
            using IConnection connection = factory.CreateConnection();
            //Kanal oluşturuyoruz.
            using IModel channel = connection.CreateModel();

            //Kuyruk oluşturuyoruz , bu kısımn publisher ile aynı olmalıdır.
            channel.QueueDeclare(queue: "example-queue", exclusive: false, autoDelete: false,durable:true);

            //Consumer oluşturuyoruz.
            EventingBasicConsumer consumer = new(channel);
            //Kuyruğu dinlemeye başlıyoruz. queue: kuyruk adı, autoAck: mesajın işlendikten sonra kuyruktan silinip silinmeyeceğini belirler, consumer: mesajın işleneceği consumer'ı belirler
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
