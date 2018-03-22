using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Praca_Inzynierska
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddMed : ContentPage
	{
        private SQLiteAsyncConnection _conntection;

        public AddMed ()
		{
			InitializeComponent ();
            today_data.Date = DateTime.Today;
            last_data.Date = DateTime.Today.AddDays(2);

            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection(); // "brama" do SQL - za pomocą tego można dodawać, usuwać, zmieniać
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            var _StartDate = today_data.Date.ToString("dd-MM-yyyy");
            var _StopDate = last_data.Date.ToString("dd-MM-yyyy");
            var _notify = notify.Time.ToString();
            var recipe = new Recipe {
                Name = title.Text,
                Dosage =dosage.Text,
                StartDate =_StartDate,
                StopDate = _StopDate,
                Notify =_notify,
                Harmonogram =notifier.On };

            if (recipe.Name == null || dosage.Text == null)
                await DisplayAlert("Błąd danych!", "Nie zostały wypełnione wszystkie dane dotyczące nowego rekordu!", "OK");
            else
            {
                await _conntection.InsertAsync(recipe);
                await Navigation.PopAsync();
            }

        }
    }
    
}