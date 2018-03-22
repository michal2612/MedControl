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

namespace Praca_Inzynierska
{
    public class Rehabilitation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string _name
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
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string Notify { get; set; }
        public bool Harmonogram { get; set; }
        public int Excercises { get; set; }
        public int Series { get; set; }
        public string ImagePath { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCo4jUZhDHlK9iTRTANP5UXiAEJeb3o231apgIxSN-iqDkZDYVYQ";
    }
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Rehab : ContentPage
	{
        private SQLiteAsyncConnection _conntection;
        private ObservableCollection<Rehabilitation> _rehabilitation;

        public Rehab ()
		{
			InitializeComponent ();
            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        protected override async void OnAppearing()
        {
            await _conntection.CreateTableAsync<Rehabilitation>();
            var rehabilitation = await _conntection.Table<Rehabilitation>().ToListAsync();
            _rehabilitation = new ObservableCollection<Rehabilitation>(rehabilitation);
            lista.ItemsSource = _rehabilitation;
            base.OnAppearing();
        }

        async private void Move_AddRehab(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRehab());
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var rehabilitation = (sender as MenuItem).CommandParameter as Rehabilitation;
            //var rekord = string.Format("Czy na pewno chcesz usunąć rekord {0}?", recipe.Name);
            //var answer = DisplayAlert("Question?", rekord, "Tak", "Nie");
            _conntection.DeleteAsync(rehabilitation); //?
            _rehabilitation.Remove(rehabilitation);
        }

        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            lista.SelectedItem = null;
        }

        private void lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var rehabilitation = e.Item as Rehabilitation;
            string harmonogram;
            if (rehabilitation.Harmonogram == true)
            {
                harmonogram = "TAK";
            }
            else harmonogram = "NIE";
            DisplayAlert(rehabilitation.Name,"Powtórzeń: " + rehabilitation.Excercises + "x" + rehabilitation.Series +
                "\n" + "Rozpoczęcie: " + rehabilitation.StartDate +
                "\n" + "Zakończenie: " + rehabilitation.StopDate +
                "\n" + "Przypomnienie: " + rehabilitation.Notify +
                "\n" + "Włączyć przypomnienie? " + harmonogram, "OK");
        }
    }
}