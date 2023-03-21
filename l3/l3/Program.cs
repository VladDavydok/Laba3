using System;
using System.IO;
using Newtonsoft.Json;

namespace TimeExample
{
    class Time
    {
        private int _seconds;
        private int _minutes;
        private int _hours;

        public int Seconds
        {
            get { return _seconds; }
            set { _seconds = value; }
        }

        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }

        public int Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }

        public Time(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public int DifferenceInSeconds(Time otherTime)
        {
            int totalSeconds = (Hours - otherTime.Hours) * 3600
                             + (Minutes - otherTime.Minutes) * 60
                             + (Seconds - otherTime.Seconds);
            return Math.Abs(totalSeconds);
        }

        public Time AddSeconds(int seconds)
        {
            int totalSeconds = Hours * 3600 + Minutes * 60 + Seconds + seconds;
            int newHours = totalSeconds / 3600 % 24;
            int newMinutes = totalSeconds % 3600 / 60;
            int newSeconds = totalSeconds % 60;
            return new Time(newHours, newMinutes, newSeconds);
        }

        public void SerializeToJson(string fileName)
        {
            string jsonString = JsonConvert.SerializeObject(this);
            File.WriteAllText(fileName, jsonString);
        }

        public static Time DeserializeFromJson(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Time>(jsonString);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Time t1 = new Time(10, 20, 30);
            Time t2 = new Time(11, 10, 15);
            Console.WriteLine("Рiзниця в секундах мiж часом 1 та часом 2: " + t1.DifferenceInSeconds(t2));

            Time t3 = t1.AddSeconds(120);
            Console.WriteLine("Час 1 + 120 секунд: " + t3.Hours + ":" + t3.Minutes + ":" + t3.Seconds);

            string fileName = "time.json";
            t1.SerializeToJson(fileName);
            Time t4 = Time.DeserializeFromJson(fileName);
            Console.WriteLine("Десерiалiзовано з файлу: " + t4.Hours + ":" + t4.Minutes + ":" + t4.Seconds);
        }
    }
}