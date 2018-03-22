using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;

namespace Praca_Inzynierska
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFood : ContentPage
    {
        private SQLiteAsyncConnection _conntection;
        private ObservableCollection<Food> _food;
        public int target = 5000;
        public AddFood()
        {
            InitializeComponent();
            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _conntection.CreateTableAsync<Food>();
            var dates = await _conntection.Table<Food>().ToListAsync();
            _food = new ObservableCollection<Food>(dates);
            list.ItemsSource = _food;
            base.OnAppearing();
        }

        private void SaveRecord_Clicked(object sender, EventArgs e)
        {
            var food = new Food();
            food.CalorieValue = Convert.ToInt32(CaloriesValueField.Text);
            food.FoodName = FoodNameField.Text;
            _conntection.InsertAsync(food);
            _food.Add(food);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            bool isEmpty = _food.Any();
            if (isEmpty)
            {
                var caloria = new Calorie()
                {
                    Today = DateTime.Today.ToString("d"),
                    Target = target
                };
                caloria.FoodList = _food.ToList();
                int calory = caloria.FoodList.Sum(m => m.CalorieValue);
                caloria.DailyCalory = calory;
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Błąd", "Podaj wymagane wartości!", "OK");
            }
        }

        async private void Delete_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selected = list.SelectedItem;
                await _conntection.DeleteAsync(selected);
                _food.Remove(selected as Food);
            }
            catch (Exception)
            {
                await DisplayAlert("Błąd!", "Nie udało się usunąć rekordu z bazy!", "OK");
            }
        }

    }
}