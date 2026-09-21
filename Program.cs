using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumerDisneyIdApi
{
    public class PersonagemData
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
    }

    public class DisneyResponse
    {
        [JsonPropertyName("data")]
        public PersonagemData Data { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://api.disneyapi.dev/character/423";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseString = await response.Content.ReadAsStringAsync();

                    DisneyResponse disneyResponse = JsonSerializer.Deserialize<DisneyResponse>(responseString);

                    if (disneyResponse != null && disneyResponse.Data != null)
                    {
                        Console.WriteLine("Nome:");
                        Console.WriteLine(disneyResponse.Data.Name);
                        Console.WriteLine();
                        Console.WriteLine("Imagem:");
                        Console.WriteLine(disneyResponse.Data.ImageUrl);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Aconteceu um erro ao consultar a api: " + e.Message);
                }
            }
        }
    }
}
