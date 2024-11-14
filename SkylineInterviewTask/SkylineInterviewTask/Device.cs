using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylineInterviewTask
{
    public class Device
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public NIC Nic { get; set; }

        public Device(string name, string model, NIC nic)
        {
            Name = name;
            Model = model;
            Nic = nic;
        }
    }
}
