using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Homework_SysPr_2
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }

    class Room
    {
        private int _roomNumber;
        private Thread _thread;
        private static readonly Random _random = new Random();
        private bool _isRunning;

        public Room(int roomNumber)
        {
            _roomNumber = roomNumber;
            _thread = new Thread(MonitorTemperature);
            _isRunning = true;
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private void MonitorTemperature()
        {
            while (_isRunning)
            {
                int temperature = _random.Next(-10, 35);
                Console.WriteLine($"Комната {_roomNumber}: Температура = {temperature}C");
                int sleepTime = _random.Next(2, 6) * 1000;
                Thread.Sleep(sleepTime);
            }
        }

        public class Program
        {
            static void Main()
            {
                List<Room> rooms = new List<Room>();
                for (int i = 0; i < 5; i++)
                {
                    rooms.Add(new Room(i));
                }
                foreach (Room room in rooms)
                {
                    room.Start();
                }

                Thread.Sleep(30000);//Работа программы в течении 30 сек

                foreach (Room room in rooms)
                { 
                    room.Stop();
                }

                Console.WriteLine("Мониторинг завершен.");
            }
        }
    }
}

