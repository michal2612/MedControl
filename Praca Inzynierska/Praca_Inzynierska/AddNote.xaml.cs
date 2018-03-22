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
	public partial class AddNote : ContentPage
	{
        private SQLiteAsyncConnection _connection;

		public AddNote ()
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        public AddNote(Notatki notatka)
            : this()
        {
            editor.Text = notatka.TextNote;
            editor.BackgroundColor = Color.White;
        }

        private void editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            editor.BackgroundColor = Color.FromHex("#a4eaff");
        }

        private void editor_Completed(object sender, EventArgs e)
        {
            editor.BackgroundColor = Color.White;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var notatka = new Notatki()
            {
                TextNote = editor.Text,
                Data = DateTime.Today.ToString("d")
            };
            _connection.InsertAsync(notatka);
            Navigation.PopAsync();
        }
    }
}