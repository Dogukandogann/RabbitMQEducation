//Bağlantı oluşturma
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://aoldppsp:f6YNTlBi7kuoVqHotf1lloh6H9wgd-sx@toad.rmq.cloudamqp.com/aoldppsp");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Kuyruk oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Mesaj gönderme
//RabbitMq kuyruğa atacağı mesajları byte olarak kabul etmektedir.
byte[] message = Encoding.UTF8.GetBytes("Hello World!");
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

Console.Read();