using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub
{
    public class Device
    {
        public string Name { get; set; }
        public string Room { get; set; }
        public int Value { get; set; }
        public double Consumption { get; set; }
        public bool IsTurnedOn { get; set; }

        public Device(string name, string room, int value, double consumption, bool isTurnedOn)
        {
            Name = name;
            Room = room;
            Value = value;
            Consumption = consumption;
            IsTurnedOn = isTurnedOn;
        }



        public override string ToString()
        {
            return $"{Name}";
        }

        public double MonthlyUsage => Value * 100;

    }
}
