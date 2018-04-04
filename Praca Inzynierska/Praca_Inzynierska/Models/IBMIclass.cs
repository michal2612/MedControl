using System.ComponentModel;

namespace Praca_Inzynierska
{
    public interface IBMIclass
    {
        float Bmi { get; set; }
        string Date { get; set; }
        int Height { get; set; }
        int Id { get; set; }
        int Weight { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}