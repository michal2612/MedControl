using SQLite;
using System.Collections.Generic;

namespace Praca_Inzynierska
{
    public class Calorie 
    {
        public Calorie()
        {
            var CalorieList = new List<Food>();
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Target { get; set; }
        public int DailyCalory { get; set; }
        public string Today { get; set; }
        public string Color = "Red";
        public List<Food> FoodList;

    }
}