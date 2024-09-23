using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri =new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Kuyruk Oluşturma
//Consumerda'da kuyruk publisher'daki ile birebir aynı yapılandırma tanımlanmalıdır.
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Mesaj okuma

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue",true,consumer);
consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlenidği yer
    //e.Body : kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span() veya e.Body.ToArray() mesajın byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
Console.Read();
