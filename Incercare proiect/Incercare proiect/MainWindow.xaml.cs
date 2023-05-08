using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebAPI;

namespace Incercare_proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool isFilling = false;
        private double sliderValue = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void SetBulbColor(Brush color, params Border[] bulbs)
        {
            foreach (var bulb in bulbs)
            {
                bulb.Background = color;
            }
        }

        private void ButtonS0_Click(object sender, RoutedEventArgs e)
        {
            SetBulbColor(Brushes.Black, BecS1, BecS2, BecS3, BecS4);
        }

        private void ButtonS1_Click(object sender, RoutedEventArgs e)
        {
            SetBulbColor(Brushes.Red, BecS1);
        }

        private void ButtonS2_Click(object sender, RoutedEventArgs e)
        {
            SetBulbColor(Brushes.Red, BecS2);
        }


        private void ButtonS3_Click(object sender, RoutedEventArgs e)
        {
            SetBulbColor(Brushes.Red, BecS3);
        }

        private void ButtonS4_Click(object sender, RoutedEventArgs e)
        {
            SetBulbColor(Brushes.Red, BecS4);
        }

        private void ButtonS5_Click(object sender, RoutedEventArgs e)
        {
            SetBulbColor(Brushes.Black, BecS1);
            SetBulbColor(Brushes.Black, BecS2);
            SetBulbColor(Brushes.Black, BecS3);
            SetBulbColor(Brushes.Black, BecS4);

        }

        private async void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderValue = e.NewValue;
            double numOfCells = Math.Floor(sliderValue * 0.23);
            Apa.Visibility = Visibility.Visible;
            if (numOfCells > 0)
            {
                double currentRow = 30 - numOfCells;
                Grid.SetRow(Apa, Convert.ToInt32(currentRow));
                Grid.SetRowSpan(Apa, Convert.ToInt32(numOfCells));

                if (currentRow <= Grid.GetRow(senzor2))
                {
                    BecS2.Background = Brushes.Red;
                }
                else
                {
                    BecS2.Background = Brushes.Black;
                }
            }
            else
            {
                Apa.Visibility = Visibility.Hidden;
            }
            await SendDataToWebAPI(sliderValue);
        }


        private async Task SendDataToWebAPI(double humidity)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { Humidity = humidity }), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost/api/simulator", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Eroare la trimiterea datelor către WebAPI");
                }
            }
        }
    }
}