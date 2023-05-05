using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        private void ButtonS0_Click(object sender, RoutedEventArgs e)
        {
            BecS1.Background = Brushes.Black;
            BecS2.Background = Brushes.Black;
            BecS3.Background = Brushes.Black;
            BecS4.Background = Brushes.Black;
        }

        private void ButtonS1_Click(object sender, RoutedEventArgs e)
        {
            BecS1.Background = Brushes.Red;
        }

        private void ButtonS2_Click(object sender, RoutedEventArgs e)
        {
            BecS2.Background = Brushes.Red;
        }

        private void ButtonS3_Click(object sender, RoutedEventArgs e)
        {
            BecS3.Background = Brushes.Red;
        }

        private void ButtonS4_Click(object sender, RoutedEventArgs e)
        {
            BecS4.Background = Brushes.Red;
        }

        private void ButtonS5_Click(object sender, RoutedEventArgs e)
        {
            BecS1.Background = Brushes.Black;
            BecS2.Background = Brushes.Black;
            BecS3.Background = Brushes.Black;
            BecS4.Background = Brushes.Black;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sliderValue = e.NewValue;
            double numOfCells = Math.Floor(sliderValue * 0.23);
            Apa.Visibility = Visibility.Visible;
            if(numOfCells > 0)
            {
                double currentRow = 30 - numOfCells;
                Grid.SetRow(Apa, Convert.ToInt32(currentRow));
                Grid.SetRowSpan(Apa, Convert.ToInt32(numOfCells));

                if(currentRow <= Grid.GetRow(senzor2))
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
        }
    }
}
