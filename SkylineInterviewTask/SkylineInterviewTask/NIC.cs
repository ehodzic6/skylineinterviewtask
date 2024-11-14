using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylineInterviewTask
{
    public class NIC
    {
        public string Description { get; set; }
        public string MacAdress { get; set; }
        public string Timestamp { get; set; }
        public long Rx { get; set; }
        public long Tx { get; set; }

        public NIC(string description, string macAdress, string timestamp, long rx, long tx)
        {
            Description = description;
            MacAdress = macAdress;
            Timestamp = timestamp;
            Rx = rx;
            Tx = tx;
        }
    }
}
