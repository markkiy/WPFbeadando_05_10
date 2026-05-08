using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Device> Devices { get; set; }

        private Device? selectedDevice;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Device? SelectedDevice {
            get => selectedDevice;
            set
            {
                selectedDevice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDevice)));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Devices = [];
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new();
            var result = of.ShowDialog();
            of.Filter = ".txt|*.txt";
            if (result != true) return;
            using StreamReader r = new(of.FileName);
            while (!r.EndOfStream)
            {
                string[] temp = r.ReadLine()!.Split(";");
                bool IsOn = temp[4] == "1";
                Device d = new(temp[0], temp[1], int.Parse(temp[2]), double.Parse(temp[3]), IsOn);
                Devices.Add(d);
            }

        }

    }
}