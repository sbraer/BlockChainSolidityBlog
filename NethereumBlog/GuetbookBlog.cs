using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;

namespace NethereumBlog
{
    internal class GuestbookBlog
    {
        internal static async Task GetMessagesAsync()
        {
            var url = "http://127.0.0.1:7545"; // <- rpc url from ganache
            var web3 = new Web3(url);
            var myContractAddress = "0xB130778f573164B8ffb8dB230A0fbCF329600530"; // <- update this
            string abi = GetAbi();
            var contract = web3.Eth.GetContract(abi, myContractAddress);

            var getMessageFunction = contract.GetFunction("GetMessages");
            var messages = await getMessageFunction.CallAsync<List<string>>();

            foreach (var msgObj in messages)
            {
                Console.WriteLine($"{msgObj}");
            }
        }

        internal static async Task WriteMessageAsync()
        {
            var url = "http://127.0.0.1:7545"; // <- rpc url from ganache
            var privateKey = "c19a8e62f637ccc3868eba2af75b6745a24dadecc01a31817a26485b79bf4225"; // <- from ganache
            var fromAddress = "0xaB7bE0c83f740176778F1833924Dd4D93ff125F8"; // <- update this
            var account = new Account(privateKey, new BigInteger(12345));
            var web3 = new Web3(account, url);
            web3.TransactionManager.UseLegacyAsDefault = true;
            var myContractAddress = "0x824f5FA5d310a6610762029338D973B17e011880"; // <- update this
            string abi = GetAbi();
            var contract = web3.Eth.GetContract(abi, myContractAddress);

            var setMessageFunction = contract.GetFunction("SendMessage");
            var gas = new HexBigInteger(1000000);
            var valueToTransfer = new HexBigInteger(0);
            await setMessageFunction.SendTransactionAndWaitForReceiptAsync(fromAddress, gas, valueToTransfer, null, "ciao 1");
        }

        private static string GetAbi()
        {
            return File.ReadAllText(Path.Combine("ABI","GuestbookBlog.json"));
        }
    }
}
