using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praca_Inzynierska
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Notes : ContentPage
	{
        private ObservableCollection<Notatki> notatki;
        private SQLiteAsyncConnection _connection;

		public Notes ()
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Notatki>();
            var rehabilitation = await _connection.Table<Notatki>().ToListAsync();
            notatki = new ObservableCollection<Notatki>(rehabilitation);
            list.ItemsSource = notatki;
            base.OnAppearing();
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNote());
        }

        private void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list.SelectedItem = null;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var notatka = (sender as MenuItem).CommandParameter as Notatki;
            _connection.DeleteAsync(notatka);
            notatki.Remove(notatka);
        }

        private void list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var notatka = e.Item as Notatki;
            Navigation.PushAsync(new AddNote(notatka));
        }
    }
}