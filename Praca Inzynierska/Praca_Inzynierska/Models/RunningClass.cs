using SQLite;
using System;

namespace Praca_Inzynierska
{
    public class RunningClass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TodayData { get; set; }
        public float Distance { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeStop { get; set; }

    }
}