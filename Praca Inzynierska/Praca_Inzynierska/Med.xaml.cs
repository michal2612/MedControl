using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Android.Widget;

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

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Med : ContentPage
	{
        private SQLiteAsyncConnection _conntection;
        private ObservableCollection<Recipe> _recipes;

		public Med ()
		{
			InitializeComponent ();
            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection(); //połączenie z bazą danych, brama do bazy
        }

        protected override async void OnAppearing()
        {
            await _conntection.CreateTableAsync<Recipe>();
            var recipes = await _conntection.Table<Recipe>().ToListAsync(); //pobiera wartosc z Tabeli Recipe
            _recipes = new ObservableCollection<Recipe>(recipes);
            lista.ItemsSource = _recipes;
            base.OnAppearing();
        }

        async private void move_AddMed(object sender, EventArgs e) //przenosi do strony z lekaami
        {
            await Navigation.PushAsync(new AddMed());
        }

        private async void Change(object sender, SelectedItemChangedEventArgs e)
        {
            var recipe = _recipes[0];
            recipe.Name += "UPDATE";
            await _conntection.UpdateAsync(recipe);
        }

        private void lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var recipe = e.Item as Recipe;
            string harmonogram;
            if (recipe.Harmonogram == true)
            {
                harmonogram = "TAK";
            }
            else harmonogram = "NIE";
            DisplayAlert(recipe.Name,"Dawkowanie: " + recipe.Dosage +
                "\n" + "Rozpoczęcie: " + recipe.StartDate +
                "\n" + "Zakończenie: " + recipe.StopDate +
                "\n" + "Przypomnienie: " + recipe.Notify +
                "\n" + "Włączyć przypomnienie? " + harmonogram, "OK");
        }

        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lista.SelectedItem = null;
        }

        private void MenuItem_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var recipe = menuItem.CommandParameter as Recipe;
            Navigation.PushAsync(new EditMed(recipe));
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var recipe = (sender as MenuItem).CommandParameter as Recipe;
            _conntection.DeleteAsync(recipe);
            _recipes.Remove(recipe);
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var name = await DisplayActionSheet("Opcje", "Anuluj", null, "Opcja 1", "Opcja 2", "Opcja 3", "Opcja 4");
            switch(name)
            {
                case "Anuluj":
                        break;
                case "Opcja 1":
                    await DisplayAlert("Witaj", "Witaj", "Witaj");
                    break;
            }
        }
    }
}