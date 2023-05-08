using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DataModel;

public class ViewModel : INotifyPropertyChanged
{
    private HttpClient _httpClient;
    private bool _sensor1Status;
    private bool _sensor2Status;
    private bool _sensor3Status;
    private bool _sensor4Status;
    private double _waterLevelStatus;
    private ICommand _sendStampCommand;
    private Sender _sender;
    private Receiver _receiver;

    public bool Sensor1Status
    {
        get => _sensor1Status;
        set
        {
            _sensor1Status = value;
            OnPropertyChanged(nameof(Sensor1Status));
        }
    }

    public bool Sensor2Status
    {
        get => _sensor2Status;
        set
        {
            _sensor2Status = value;
            OnPropertyChanged(nameof(Sensor2Status));
        }
    }

    public bool Sensor3Status
    {
        get => _sensor3Status;
        set
        {
            _sensor3Status = value;
            OnPropertyChanged(nameof(Sensor3Status));
        }
    }

    public bool Sensor4Status
    {
        get => _sensor4Status;
        set
        {
            _sensor4Status = value;
            OnPropertyChanged(nameof(Sensor4Status));
        }
    }

    public double WaterLevelStatus
    {
        get => _waterLevelStatus;
        set
        {
            _waterLevelStatus = value;
            OnPropertyChanged(nameof(WaterLevelStatus));
        }
    }


    public ICommand SendStampCommand
    {
        get
        {
            if (_sendStampCommand == null)
            {
                _sendStampCommand = new RelayCommand(async _ => await SendStampAsync());
            }

            return _sendStampCommand;
        }
    }


    public ViewModel()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5157/api/") };
        _sender = new Sender("127.0.0.1", 5157); // Alege un port potrivit pentru tine.
        _receiver = new Receiver(5157);

    }

    public async Task<Task> SendStampAsync()
    {
        Stamp stamp = new Stamp
        {
            Timestamp = DateTime.Now,
            Sensor1 = Sensor1Status,
            Sensor2 = Sensor2Status,
            Sensor3 = Sensor3Status,
            Sensor4 = Sensor4Status,
            WaterLevel = WaterLevelStatus
        };

        using HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5157");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            await _sender.SendAsync(stamp);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la trimiterea stamp-ului: {ex.Message}");
        }
        return Task.CompletedTask;
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
