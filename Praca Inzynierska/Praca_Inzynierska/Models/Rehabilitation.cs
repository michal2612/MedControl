using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Praca_Inzynierska
{
    public class Rehabilitation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string _name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;

                _name = value;
                OnPropertyChanged();
            }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //jeśli PropertyChanged = null nic nierób, jeżeli inna wartość funkcja invoke
        }
        [MaxLength(255)]
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string Notify { get; set; }
        public bool Harmonogram { get; set; }
        public int Excercises { get; set; }
        public int Series { get; set; }
        public string ImagePath { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCo4jUZhDHlK9iTRTANP5UXiAEJeb3o231apgIxSN-iqDkZDYVYQ";
    }
}