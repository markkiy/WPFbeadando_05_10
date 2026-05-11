using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartHub
{
    public class Device : INotifyPropertyChanged
    {

        private string name;
        private string room;
        private int valueertek;
        private double consumption;
        private string settingType;
        private bool isTurnedOn;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string Room
        {
            get => room;
            set
            {
                room = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Room)));
            }
        }
        public int Value
        {
            get => valueertek;
            set
            {
                valueertek = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MonthlyUsage)));

            }
        }
        public double Consumption
        {
            get => consumption;
            set
            {
                consumption = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Consumption)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MonthlyUsage)));

            }
        }
        public string SettingType
        {
            get => settingType;
            set
            {
                settingType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SettingType)));
            }
        }
        public bool IsTurnedOn
        {
            get => isTurnedOn;
            set
            {
                isTurnedOn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTurnedOn)));
            }
        }

        public Device(string name, string room, int valueertek, double consumption, string settingtype, bool isTurnedOn)
        {
            Name = name;
            Room = room;
            Value = valueertek;
            Consumption = consumption;
            SettingType = settingtype;
            IsTurnedOn = isTurnedOn;
        }

        public Device() {
            Name = "";
            Room = "";
            Value = 0;
            Consumption = 0;
            SettingType = "";
            IsTurnedOn = false;

        }



        public override string ToString()
        {
            return $"{Name}";
        }

        public double MonthlyUsage => Math.Round((Value / 1.5) * Consumption * 70, 2);

    }
}
