using Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Basic Mesaj Okuma - mesaj okunur ve onay bildirimi gönderilmez , autoack false olduğu durumlarda mesaj onaylanmazsa kuyruktan silinmez.
BasicConsume.BasicConsumer();

////Basic Ack Mesaj Okuma , onay bildirimi gönderildiği için mesajlar kuyruktan silinir.
//BasicAck.BasicAckConsume();

////Direct Exchange Mesaj Okuma -- Mesajlar routing key ile belirtilen kuyruklardan okunur.
//DirectExchangeConsume.Consume();

////Fanout Exchange Mesaj Okuma -- Mesajlar fanout exchange'e gönderilir ve bu exchange'e bağlı tüm kuyruklardan mesajlar okunur.
//FanoutConsume.Consume();

////Header Exchange Mesaj Okuma -- Mesajlar header exchange'e gönderilir ve bu exchange'e bağlı kuyruklardan mesajlar okunur.
//HeaderExchangeConsume.Consume();

////Topic Exchange Mesaj Okuma -- Mesajlar topic exchange'e gönderilir ve bu exchange'e bağlı kuyruklardan mesajlar okunur.
//TopicExchangeConsume.Consume();

Console.Read();
