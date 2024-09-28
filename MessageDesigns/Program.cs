using MessageDesigns.P2P;
using MessageDesigns.PublishSubscribe;
using MessageDesigns.RequestReponse;
using MessageDesigns.WorkQueue;

namespace MessageDesigns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //P2pPublisher.Publish();


            //P2pConsumer.Consume();


            //PublishSubPublisher.Publish();

            //PublishSubConsumer.Consume();

            //WorkQueuePublisher.Publish();

            //WorkQueueConsumer.Consume();

            RequestReponsePublisher.Publish();

            RequestReponseConsumer.Consume();

            Console.ReadLine();

        }
    }
}
