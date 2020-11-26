using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using TccDev.gRPC.Protos;

namespace TccConsumo.ConsolegRPC
{
    class Program
    {
        private const string servidor = "https://localhost:5001";
        private static async Task ListarProdutos(
            ProdutogRPC.ProdutogRPCClient client)
        {
            Console.WriteLine("Lista de produtos:");
            using (var call = client.Listar(new ListarProdutosRequest()))
            {
                var responseStream = call.ResponseStream;

                CancellationTokenSource cts = new CancellationTokenSource();
                var token = cts.Token;

                while (await responseStream.MoveNext(token))
                {
                    var dadosProduto = responseStream.Current.Produto;
                    Console.WriteLine(dadosProduto.ProdutoId + " | " + dadosProduto.Descricao + " | ");
                }
            }

            Console.WriteLine();
        }
        public static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress(servidor);
            var client = new ProdutogRPC.ProdutogRPCClient(channel);

            await ListarProdutos(client);
        }
    }
}
