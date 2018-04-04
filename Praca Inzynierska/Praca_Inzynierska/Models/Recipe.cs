using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Praca_Inzynierska
{
    public class Recipe : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;

        [MaxLength(255)]
        public string Name
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
        public string Dosage { get; set; }
        [MaxLength(255)]
        public string ImagePath { get; set; } = "http://www.thefilipinodoctor.com/imagesnew/nopicdrug.png";
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string Notify { get; set; }
        public bool Harmonogram { get; set; }

    }
}