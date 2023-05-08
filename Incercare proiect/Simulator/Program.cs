using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Simulator
{
    internal class Program
    {
        public class SensorData
        {
            public double WaterLevel { get; set; }
            public bool[] SensorStatus { get; set; }
        }

        private static SensorData SimulateData()
        {
            // Aici, simulează datele în funcție de logica specifică a proiectului
            // De exemplu:
            Random random = new Random();
            SensorData data = new SensorData
            {
                WaterLevel = random.NextDouble() * 100,
                SensorStatus = new[] { random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0 }
            };

            return data;
        }

        private static async Task SendDataToAPI(SensorData data)
        {
            string apiUrl = "https://your-api-url/api/sensor-data"; // Înlocuiește cu URL-ul corect al API-ului tău

            using (HttpClient httpClient = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
            }
        }

        static async Task Main(string[] args)
        {
            while (true)
            {
                SensorData data = SimulateData();
                await SendDataToAPI(data);

                // Așteaptă o perioadă de timp înainte de a trimite următoarea serie de date
                await Task.Delay(TimeSpan.FromSeconds(10)); // Ajustează intervalul de timp în funcție de nevoi
            }
        }

    }
}
