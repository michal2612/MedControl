using SQLite;
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
	public partial class AddRehab : ContentPage
	{
        private SQLiteAsyncConnection _conntection;

        public AddRehab ()
		{
			InitializeComponent ();

            today_data.Date = DateTime.Today;
            last_data.Date = DateTime.Today.AddDays(2);

            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        async private void Add_Clicked(object sender, EventArgs e)
        {
            var _StartDate = today_data.Date.ToString("dd-MM-yyyy");
            var _StopDate = last_data.Date.ToString("dd-MM-yyyy");
            var _notify = notify.Time.ToString();
            var _Excercise = Convert.ToInt32(excercise.Text);
            var _Series = Convert.ToInt32(excercise_1.Text);
            var recipe = new Rehabilitation {
                Name = title.Text,
                StartDate = _StartDate,
                StopDate = _StopDate,
                Notify = _notify,
                Harmonogram = notifier.On,
                Excercises = _Excercise,
                Series = _Series };

            if (recipe.Name == null || excercise.Text == null || excercise_1.Text == null)
                await DisplayAlert("Błąd danych!", "Nie zostały wypełnione wszystkie dane dotyczące nowego rekordu!", "OK");
            else
            {
                await _conntection.InsertAsync(recipe);
                await Navigation.PopAsync();
            }
        }
    }
}