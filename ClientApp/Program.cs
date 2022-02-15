// See https://aka.ms/new-console-template for more information
using IO.Swagger;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System.Net.Http.Headers;

class Program
{
    static void Main(string[] args)
    {
        var service = new AccountApi("https://localhost:7047/");
        var serviceWeather = new WeatherApi("https://localhost:7047/");
        ApiClient apiClient = new ApiClient("https://localhost:7047/");

        var token = service.ApiAcountLoginPost(new Login()
        {
            Email = "string@test.test",
            Password = "string"
        });
        //conf.DefaultHeader("Authorization", $"Bearer {token}");
        var conf = new Configuration();
        conf.AddApiKey("Authorization", $"Bearer {token}");
        apiClient.Configuration = conf;
        var listaPogody = serviceWeather.WeatherGet();

        var item = serviceWeather.WeatherIdGet(1);

        //foreach (var item in listaPogody)
        {
            Console.WriteLine(item.ToString());
        }
        Console.ReadLine();
    }
}