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
	public partial class EditMed : ContentPage
	{
        private SQLiteAsyncConnection _conntection;

        public EditMed (Recipe recipe)
		{
            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection();
            BindingContext = recipe ?? throw new ArgumentNullException();

			InitializeComponent ();

            today_data.Date = DateTime.ParseExact(recipe.StartDate, "dd-MM-yyyy", null);
            last_data.Date = DateTime.ParseExact(recipe.StopDate, "dd-MM-yyyy", null);
            notify.Time = TimeSpan.Parse(recipe.Notify);
            harmonogram(recipe);
        }
        public void harmonogram(Recipe recipe)
        {
            if (recipe.Harmonogram == true)
                notifier.On = true;
            else
                notifier.On = false;
        }

        async private void Saved(object sender, EventArgs e)
        {
            var _StartDate = today_data.Date.ToString("dd-MM-yyyy");
            var _StopDate = last_data.Date.ToString("dd-MM-yyyy");
            var _notify = notify.Time.ToString();
            //await _conntection.UpdateAsync(recipe);
            await DisplayAlert("", "Zapisano zmiany!", "OK");
            await Navigation.PopAsync();
        }
    }
}