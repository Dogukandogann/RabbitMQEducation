//Bağlantı oluşturma
using Publisher;
using RabbitMQ.Client;
using System.Text;

//Basic Publish 
BasicPublish.Publish();

//Message Durability Publish -- basic publisten farklı olarak rabbitmq tarafında bir problemle(Server çökmeleri v.s) karşılaşsak bile kuyruk ve mesaj kalıcı olacaktır.%100 garantili bir koruma  sağlamaz.Bunun için outbox , inbox gibi mimariler kullanılabilir.
MessageDurabilityPublish.Publish();

Console.Read();