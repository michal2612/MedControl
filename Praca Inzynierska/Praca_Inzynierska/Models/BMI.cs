using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Praca_Inzynierska
{
    public class BMI : INotifyPropertyChanged, IBMIclass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public float _Bmi;
        public float Bmi
        {
            get { return _Bmi; }
            set
            {
                if (_Bmi == value)
                    return;

                _Bmi = value;
                OnPropertyChanged();
            }
        }
        public string Date { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //jeśli PropertyChanged = null nic nierób, jeżeli inna wartość funkcja invoke
        }
    }
}