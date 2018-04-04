using SQLite;

namespace Praca_Inzynierska
{
    public class Notatki
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TextNote { get; set; }
        public string Data { get; set; }
    }
}