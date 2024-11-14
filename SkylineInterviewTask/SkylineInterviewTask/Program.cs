using SkylineInterviewTask;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main()
    {
        var bitsDataCalculator = new BitsDataCalculator();
        Console.WriteLine("Device is initialized!");

        while (true)
        {
            Console.WriteLine("\n0 - Exit\n1 - Start sending requests\n2 - Reboot device\n");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    StartSendingRequests(bitsDataCalculator);
                    break;
                case 2:
                    bitsDataCalculator.ResetDevice();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select 0, 1, or 2.");
                    break;
            }
        }
    }

    static void StartSendingRequests(BitsDataCalculator bitsDataCalculator)
    {
        Console.WriteLine("Press 'Q' to stop!");

        while (true)
        {
            if (Console.KeyAvailable && Console.ReadKey(intercept: true).Key == ConsoleKey.Q)
                break;

            bitsDataCalculator.SendRequest();
            Thread.Sleep(500);
        }

        bitsDataCalculator.WriteResults();
    }
}
