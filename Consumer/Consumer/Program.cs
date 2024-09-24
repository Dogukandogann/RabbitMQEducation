using Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Basic Mesaj Okuma - mesaj okunur ve onay bildirimi gönderilmez , autoack false olduğu durumlarda mesaj onaylanmazsa kuyruktan silinmez.
BasicConsume.BasicConsumer();

//Basic Ack Mesaj Okuma , onay bildirimi gönderildiği için mesajlar kuyruktan silinir.
BasicAck.BasicAckConsume();

Console.Read();
