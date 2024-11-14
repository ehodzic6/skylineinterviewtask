using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylineInterviewTask
{
    public class BitsDataCalculator
    {
        public Device Device { get; set; }
        public List<BitsData> BitsDataHistory { get; set; }
        public BitsDataCalculator()
        {
            string json = File.ReadAllText("../../../Data.json");

            JObject deviceData = JObject.Parse(json);

            string deviceName = deviceData["Device"].ToString();
            string model = deviceData["Model"].ToString();

            foreach (var nicData in deviceData["NIC"])
            {
                NIC nic = new NIC(nicData["Description"].ToString(),
                    nicData["MAC"].ToString(),
                    nicData["Timestamp"].ToString(),
                    long.Parse(nicData["Rx"].ToString()),
                    long.Parse(nicData["Tx"].ToString()));
                this.Device = new Device(deviceName, model, nic);
            }
            this.BitsDataHistory = new List<BitsData>();
        }
        public BitsData CalculateBitsData(long oldRx, long oldTx, long newRx, long newTx, int networkCongestion)
        {
            double txBitrate = (newTx - oldTx) * 8 / (0.5 + (networkCongestion * 1.0) / 1000);
            double rxBitrate = (newRx - oldRx) * 8 / (0.5 + (networkCongestion * 1.0) / 1000);

            Console.WriteLine("Total number of transmitted bytes: " + newTx + " Tx current Bitrate: " + Math.Round(txBitrate, 2));
            Console.WriteLine("Total number of received bytes: " + newRx + " Rx current Bitrate: " + Math.Round(rxBitrate, 2));
            Console.WriteLine("_______________________________________________________________________");
            return new BitsData(newTx, newRx, rxBitrate, txBitrate);

        }
        public void SendRequest()
        {
            long oldRx = Device.Nic.Rx;
            long oldTx = Device.Nic.Tx;
            Random random = new Random();
            var firstRandom = random.Next(3500, 7501);
            var secondRandom = random.Next(1500, 3501);
            if (Device.Nic.Rx > long.MaxValue - firstRandom)
            {
                Device.Nic.Tx = 0;
                Device.Nic.Rx = Device.Nic.Rx - Device.Nic.Tx;
            }
            if (Device.Nic.Tx > long.MaxValue - secondRandom)
            {
                Device.Nic.Tx = 0;
                Device.Nic.Rx = Device.Nic.Rx - Device.Nic.Tx;
            }
            Device.Nic.Rx += firstRandom;
            Device.Nic.Tx += secondRandom;
            var networkCongestion = random.Next(50, 501);
            Thread.Sleep(networkCongestion);
            BitsDataHistory.Add(CalculateBitsData(oldRx, oldTx, Device.Nic.Rx, Device.Nic.Tx, networkCongestion));

        }
        public void WriteResults()
        {
            double txSum = 0;
            double rxSum = 0;
            foreach (var BitsData in BitsDataHistory)
            {
                txSum += BitsData.TxBitsData;
                rxSum += BitsData.RxBitsData;
            }
            var txAverage = txSum / BitsDataHistory.Count;
            var rxAverage = rxSum / BitsDataHistory.Count;
            Console.WriteLine("Average Tx Bitrate: " + Math.Round(txAverage, 2));
            Console.WriteLine("Average Rx Bitrate: " + Math.Round(rxAverage, 2));
        }
        public void ResetDevice()
        {
            string json = File.ReadAllText("../../../Data.json");

            JObject deviceData = JObject.Parse(json);

            string deviceName = deviceData["Device"].ToString();
            string model = deviceData["Model"].ToString();

            foreach (var nic in deviceData["NIC"])
            {
                NIC nico = new NIC(nic["Description"].ToString(),
                    nic["MAC"].ToString(),
                    nic["Timestamp"].ToString(),
                    0,
                    0);
                this.Device = new Device(deviceName, model, nico);
            }
            this.BitsDataHistory = new List<BitsData>();
        }
    }
}
