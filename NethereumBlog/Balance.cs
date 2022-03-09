using Nethereum.Web3;

namespace NethereumBlog
{
    internal class Balance
    {
        internal static async Task ShowBalanceDemoAsync()
        {
            var url = "http://127.0.0.1:7545"; // <- rpc url from ganache
            string address = "0x2214fD8cADBbE3edE5B95a37A88951b6Fa1D5d58"; // < - public address from ganache
            var web3 = new Web3(url); // RPC
            var balance = await web3.Eth.GetBalance.SendRequestAsync(address);
            Console.WriteLine($"Balance in Wei: {balance.Value}");

            var etherAmount = Web3.Convert.FromWei(balance.Value);
            Console.WriteLine($"Balance in Ether: {etherAmount}");
        }
    }
}
