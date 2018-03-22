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
    public class RunningClass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TodayData { get; set; }
        public float Distance { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeStop { get; set; }

    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Running : ContentPage
	{
        public SQLiteAsyncConnection _connection;
        private ObservableCollection<RunningClass> _running;

		public Running ()
		{
			InitializeComponent ();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<RunningClass>();
            var dates = await _connection.Table<RunningClass>().ToListAsync();
            _running = new ObservableCollection<RunningClass>(dates);
            list.ItemsSource = _running;
            base.OnAppearing();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddRun());
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var dates = (sender as MenuItem).CommandParameter as RunningClass;
            _connection.DeleteAsync(dates);
            _running.Remove(dates);
        }

        private void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list.SelectedItem = null;
        }

        private void list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as RunningClass;
            DisplayAlert("Szczegółowe informacje", "Data biegu: " + item.TodayData + "\n"
                + "Dystans: " + item.Distance + " km" + "\n"
                + "Czas biegu: " + item.Time  + "\n"
                + "Początek: " + item.TimeStart + "\n"
                + "Koniec: " + item.TimeStop,"OK");
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            DisplayAlert("Bieganie", "@Systematyczne bieganie ma pozytywny wpływ na wzmocnienie serca i układu krążenia. Ten najbardziej pracowity organ wykonuje średnio 70 skurczów na minutę, przepompowując w tym czasie 5-7 litrów krwi, czyli w ciągu godziny 4200 uderzeń i 400 litrów przepompowanej krwi, a dziennie – aż 100 000 uderzeń! I tak dzień po dniu, bez najmniejszej przerwy.W ciągu 70 lat kurczy się 2, 5 miliarda razy i przepompowuje około 180 000 litrów krwi.Energia, jaką wyzwala w ciągu życia serce człowieka, zdolna byłaby wyrzucić go na księżyc.Jak każdy inny mięsień, serce musi być trenowane, aby dobrze funkcjonowało.Jego objętość u osoby niewytrenowanej to średnio 600 - 700 cm3, natomiast wysportowanej - 1000 - 1400cm3.Te statystyczne dane przekładają się na skuteczność pracy serca i paradoksalnie pokazują, że serce osoby nie wytrenowanej musi się więcej napracować, żeby przepompować taką samą ilość krwi.", "OK");
        }
    }
}