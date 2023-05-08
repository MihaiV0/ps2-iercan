using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using DataModel;
using System.Text;

public class Sender
{
    private string _ip;
    private int _port;
    private TcpClient _client;

    public Sender(string ip, int port)
    {
        _ip = ip;
        _port = port;
    }

    public async Task SendAsync(Stamp stamp)
    {
        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://www.proiect.ro");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            string jsonContent = JsonConvert.SerializeObject(stamp);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/simulator", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la trimiterea stamp-ului: {ex.Message}");
        }
    }

}
