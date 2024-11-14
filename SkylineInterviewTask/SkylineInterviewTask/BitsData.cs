using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylineInterviewTask
{
    public class BitsData
    {
        public long RxBits { get; set; }
        public long TxBits { get; set; }
        public double RxBitsData { get; set; }
        public double TxBitsData { get; set; }

        public BitsData(long rxBits, long txBits, double rxBitsData, double txBitsData)
        {
            RxBits = rxBits;
            TxBits = txBits;
            RxBitsData = rxBitsData;
            TxBitsData = txBitsData;

        }
    }
}
