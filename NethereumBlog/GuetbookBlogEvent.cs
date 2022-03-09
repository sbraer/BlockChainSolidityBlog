using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.JsonRpc.WebSocketStreamingClient;
using Nethereum.RPC.Reactive.Eth.Subscriptions;
using Nethereum.Web3;

namespace NethereumBlog
{
    [Event("MessageInserted")]
    public class MyTestEvent : IEventDTO
    {
        [Parameter("uint", "counter", 1)]
        public ulong Counter { get; set; }

        [Parameter("string", "message", 2)]
        public string? Message { get; set; }
    }

    internal class GuestbookBlogEvent
    {
        internal static async Task WaitMessageAsync()
        {
            Console.WriteLine($"Subscribe to event...");
            var web3 = new Web3("http://127.0.0.1:7545");
            using (var client = new StreamingWebSocketClient("ws://127.0.0.1:7545")) //wss://...
            {
                var subscription = new EthLogsObservableSubscription(client);
                subscription.GetSubscriptionDataResponsesAsObservable()
                             .Subscribe(log =>
                             {
                                 try
                                 {
                                     EventLog<MyTestEvent> decoded = Event<MyTestEvent>.DecodeEvent(log);
                                     Console.WriteLine($@"{decoded.Event.Counter}: {decoded.Event.Message ?? "-"}");
                                 }
                                 catch (Exception ex)
                                 {
                                     Console.WriteLine("Log Address: " + log.Address + " is not a standard transfer log:", ex.Message);
                                 }
                             });

                var transferEventHandler = web3.Eth.GetEvent<MyTestEvent>("0xA8b38117d02c9F320752AFE97B9CC2181Ff85001"); // <- update this
                var filter = transferEventHandler.CreateFilterInput();

                await client.StartAsync();
                subscription.GetSubscribeResponseAsObservable().Subscribe(id => Console.WriteLine($"Subscribed with id: {id}"));

                await subscription.SubscribeAsync(filter);
                Console.ReadLine();

                await subscription.UnsubscribeAsync();
            }
        }
    }
}
