using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher
{
    public static class BasicPublish
    {
        public static void Publish()
        {
            //RabbitMQ sunucusuna bağlanmak için ConnectionFactory sınıfını kullanıyoruz.
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

            //Bağlantıyı aktifleştiriyoruz.
            using IConnection connection = factory.CreateConnection();
            //Kanal oluşturuyoruz.
            using IModel channel = connection.CreateModel();

            //Kuyruk oluşturuyoruz , queue: kuyruk adı, exclusive: sadece bu bağlantıya özel olup olmadığını belirler, autoDelete: son mesajın alındıktan sonra kuyruğun silinip silinmeyeceğini belirler, durable: kuyruğun kalıcı olup olmadığını belirler
            channel.QueueDeclare(queue: "example-queue", exclusive: false,autoDelete:false,durable:true);

            //Mesaj gönderiyoruz.Burda dikkat etmemiz geren nokta ise RabbitMq kuyruğa atacağı mesajları byte olarak kabul etmektedir.

            byte[] message = Encoding.UTF8.GetBytes("Hello World!");

            // BasicPublish metodu ile mesajı gönderiyoruz , exchange: mesajın hangi exchange üzerinden gönderileceğini belirler, routingKey: mesajın hangi kuyruğa gönderileceğini belirler, body: mesajın içeriğini belirler
            channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
            
            Console.WriteLine("Mesaj Başarıyla Kuyruğa İletildi.");
        }
    }
}
