//Bağlantı oluşturma
using Publisher;
using RabbitMQ.Client;
using System.Text;

//Basic Publish 
BasicPublish.Publish();

//Message Durability Publish -- basic publisten farklı olarak rabbitmq tarafında bir problemle(Server çökmeleri v.s) karşılaşsak bile kuyruk ve mesaj kalıcı olacaktır.%100 garantili bir koruma  sağlamaz.Bunun için outbox , inbox gibi mimariler kullanılabilir.
MessageDurabilityPublish.Publish();

//Direct Exchange Publish -- Mesajlar routing key ile belirtilen kuyruklara gönderilir.
DirectExchangePublish.Publisher();

//Fanout Exchange Publish -- Mesajlar fanout exchange'e gönderilir ve bu exchange'e bağlı tüm kuyruklara mesajlar gönderilir.
FanoutPublish.Publish();

//Header Exchange Publish -- Mesajlar header exchange'e gönderilir ve bu exchange'e bağlı kuyruklara gönderilir.
HeaderExchangePublish.Publish();

//Topic Exchange Publish -- Mesajlar topic exchange'e gönderilir ve bu exchange'e bağlı kuyruklara gönderilir.
TopicExchangePublish.Publish();

Console.Read();