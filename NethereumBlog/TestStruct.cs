using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace NethereumBlog
{
    internal class TestStruct
    {
        internal static async Task GetMessagesAsync()
        {
            var url = "http://127.0.0.1:7545"; // <- rpc url from ganache
            var web3 = new Web3(url);
            var myContractAddress = "0xd4BEB43A49FeF1Aec691638D66b05f91D3a49a50"; // <- update this
            string abi = GetAbi();
            var contract = web3.Eth.GetContract(abi, myContractAddress);

            var getMessageFunction = contract.GetFunction("GetMessages");
            var messages = await getMessageFunction.CallAsync<List<MessageDTO>>();

            foreach (var msgObj in messages)
            {
                Console.WriteLine($"{msgObj.Counter}: {msgObj.Message}");
            }
        }

        private static string GetAbi()
        {
            return File.ReadAllText(Path.Combine("ABI","TestEvent.json"));
        }
    }

    [FunctionOutput]
    internal class MessageDTO : IFunctionOutputDTO
    {
        [Parameter("uint", "Counter", 1)]
        public virtual int Counter { get; set; }

        [Parameter("string", "Message", 2)]
        public virtual string Message { get; set; } = null!;
    }
}
