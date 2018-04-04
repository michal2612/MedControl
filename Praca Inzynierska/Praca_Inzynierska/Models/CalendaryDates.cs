using SQLite;
using System;

using Xamarin.Forms;

namespace Praca_Inzynierska
{
    public class CalendaryDates
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public Color EventColor { get; set; }
    }
}