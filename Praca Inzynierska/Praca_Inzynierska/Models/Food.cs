using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praca_Inzynierska
{
    public class Food
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FoodName { get; set; }
        public int CalorieValue { get; set; }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calories : ContentPage
	{
        public SQLiteAsyncConnection _connection;
        public ObservableCollection<Calorie> _calories;

		public Calories ()
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }


        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Calorie>();
            var dates = await _connection.Table<Calorie>().ToListAsync();
            _calories = new ObservableCollection<Calorie>(dates);
            list.ItemsSource = _calories;
            base.OnAppearing();
        }

        private void ToolbarItem_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new AddFood());
        }


        private void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list.SelectedItem = null;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var recipe = (sender as MenuItem).CommandParameter as Calorie;
            _connection.DeleteAsync(recipe);
            _calories.Remove(recipe);
        }
    }
}