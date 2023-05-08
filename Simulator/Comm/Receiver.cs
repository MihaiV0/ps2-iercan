using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using DataModel;

public class Receiver
{
    private int _port;
    private TcpListener _listener;

    public event EventHandler<Stamp> OnDataReceived;

    public Receiver(int port)
    {
        _port = port;
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        Task.Run(() => ListenForClientsAsync());
    }

    private async Task ListenForClientsAsync()
    {
        while (true)
        {
            TcpClient client = await _listener.AcceptTcpClientAsync();
            Task.Run(async () => await HandleClientAsync(client));
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        using (client)
        {
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    using (var memoryStream = new MemoryStream(buffer, 0, bytesRead))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        Stamp data = formatter.Deserialize(memoryStream) as Stamp;
                        OnDataReceived?.Invoke(this, data);
                    }
                }
            }
        }
    }
}
