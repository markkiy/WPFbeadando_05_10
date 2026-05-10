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

        //public static Dictionary<string, string> Map = new()
        //{
        //    { "lámpa", "Fényerő" },
        //    { "led", "Fényerő" },
        //    { "termosztát", "Hőfok" },
        //    { "fűtés", "Hőfok" },
        //    { "klíma", "Hőfok" },
        //    { "konnektor", "Energia" }
        //};

        private Device selectedDevice;

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
            selectedDevice = new();
            Devices = [];
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new();
            of.Filter = "Szöveges fájlok (*.txt)|*.txt|Minden fájl (*.*)|*.*";
            var result = of.ShowDialog();

            if (result != true) return;
            try
            {
                Devices.Clear();
                using StreamReader r = new(of.FileName);
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine()!;
                    try
                    {
                        string[] temp = line.Split(";");     
                        if (temp.Length < 6)
                        {
                            throw new FormatException("A sor nem tartalmaz elég adatot.");
                        }
                        bool IsOn = temp[5] == "1"; 
                        Device d = new(temp[0], temp[1], int.Parse(temp[2]), double.Parse(temp[3]), temp[4], IsOn);
                        Devices.Add(d);
                    }
                    catch (Exception ex)
                    {
                       
                        MessageBox.Show($"Hiba történt a következő sor feldolgozásakor:\n'{line}'\n\nOka: {ex.Message}",
                                        "Adathiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (IOException ex)
            {
                
                MessageBox.Show($"Nem sikerült megnyitni a fájlt. Lehet, hogy meg van nyitva egy másik programban.\n\n:{ex.Message}",
                    "Fájlolvasási hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Váratlan hiba történt a betöltés során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void EszkozLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EszkozLista.SelectedItem == null)
            {
                selectedDevice = new();
            }

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDevice.Name == "" || selectedDevice.Room == "")
            {
                MessageBox.Show($"Váratlan hiba történt. Üres mező nem lehet!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Device tempd = new Device(selectedDevice.Name, selectedDevice.Room, selectedDevice.Value, 10, "Általános", selectedDevice.IsTurnedOn);
            Devices.Add(tempd);
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDevice != null)
            {
            Devices.Remove(SelectedDevice);

            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sf = new();
            sf.Filter = "Szöveges fájl (*.txt)|*.txt|Minden fájl (*.*)|*.*";
            var result = sf.ShowDialog();
            
            if (result != true) return;
            try
            {
                using StreamWriter sw = new(sf.FileName);
                foreach (Device item in Devices)
                {
                    string status = item.IsTurnedOn ? "1" : "0";
                    sw.WriteLine($"{item.Name};{item.Room};{item.Value};{item.Consumption};{item.SettingType};{status}");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a mentés során: {ex.Message}");
            }
            
        }
    }
}