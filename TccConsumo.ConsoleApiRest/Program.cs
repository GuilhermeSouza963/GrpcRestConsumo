using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TccConsumo.ConsoleApiRest
{
    class Program
    {
        private const string servidor = "http://localhost:52981";
        private static async Task ListarProdutos()
        {
            Console.WriteLine("Lista de produtos:");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(servidor);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/produto/listar");
                if (response.IsSuccessStatusCode)
                {  //GET
                    var retorno = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                    foreach (var item in retorno)
                    {
                        Console.WriteLine(item.produtoId + " | " + item.descricao + " | ");
                    }

                }
            }

                Console.WriteLine();
        }
        public static async Task Main(string[] args)
        {
            await ListarProdutos();
        }
    }
}
