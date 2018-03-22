using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praca_Inzynierska
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDate : ContentPage
    {
        public AddDate()
        {
            InitializeComponent();

            Dictionary<string, Color> PickerElements = new Dictionary<string, Color>() {
                {"Czerwony", Color.Red },
                {"Czarny", Color.Black  },
                {"Niebieski", Color.Blue },
                {"Zielony", Color.Green },
                {"Żółty", Color.Yellow }
            };
            EventColor.ItemsSource = PickerElements.Keys.ToList<string>();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(EventName.Text) || EventColor.SelectedItem == null)
            {
                DisplayAlert("Błąd dodania", "Uzupełnij poprawnie wszystkie pola", "OK");
            }
            else
                Navigation.PopAsync();
        }
    }
}