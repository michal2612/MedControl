using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praca_Inzynierska
{
    public class BMI : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public float _Bmi;
        public float Bmi
        {
            get { return _Bmi; }
            set
            {
                if (_Bmi == value)
                    return;

                _Bmi = value;
                OnPropertyChanged();
            }
        }
        public string Date { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //jeśli PropertyChanged = null nic nierób, jeżeli inna wartość funkcja invoke
        }
    }
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Weight : ContentPage
	{
        private SQLiteAsyncConnection _conntection;
        private ObservableCollection<BMI> _dates;

        public Weight()
        {
            InitializeComponent();
            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        protected override async void OnAppearing()
        {
            await _conntection.CreateTableAsync<BMI>();
            var dates = await _conntection.Table<BMI>().ToListAsync();
            _dates = new ObservableCollection<BMI>(dates);
            list.ItemsSource = _dates;
            base.OnAppearing();
        }

        private void oblicz_Clicked(object sender, EventArgs e)
        {
            try
            {
                var masa = double.Parse(waga.Text);
                var wysoko = Math.Pow((double.Parse(wzrost.Text) / 100), 2);
                string bmi;
                if (masa / wysoko < 18.5)
                    bmi = "Wartość BMI wskazuje niedowagę!";
                else if (masa / wysoko > 18.5 && masa / wysoko < 25)
                    bmi = "Wartość BMI wskazuje wagę prawidłową!";
                else
                    bmi = "Wartość BMI wskazuje nadwagę!";
                DisplayAlert("BMI", String.Format("Twoje BMI to: {0:F2}", masa / wysoko) + "\n" + bmi, "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Błąd danych!", "Wprowadź poprawnie wartości!", "OK");
            }
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var date = DateTime.Today.ToString("d");
                var dates = new BMI
                {
                    Height = Convert.ToInt32(wzrost.Text),
                    Weight = Convert.ToInt32(waga.Text),
                    Bmi =
                    Convert.ToInt32(waga.Text) / ((float)Math.Pow((double.Parse(wzrost.Text) / 100), 2)),
                    Date = date
                };
                await _conntection.InsertAsync(dates);
                _dates.Add(dates);
            }
            catch (Exception)
            {
                await DisplayAlert("Błąd danych!", "Wprowadź poprawnie wartości!", "OK");
            }
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            var dates = _dates[0];
            await _conntection.DeleteAsync(dates);
            _dates.Remove(dates);
        }

        private void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list.SelectedItem = null;
        }

        private void list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dates = e.Item as BMI;
            DisplayAlert("Szczegółowe informacje", "Waga: " + dates.Weight + " kg" + "\n" + "Wzrost: " + dates.Height + " cm" + "\n" + "Data: " + dates.Date,"OK");
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string information = "Wskaźnik masy ciała[1] (ang. Body Mass Index (BMI); również: wskaźnik Queteleta II) – współczynnik powstały przez podzielenie masy ciała podanej w kilogramach przez kwadrat wysokości podanej w metrach[2]. Klasyfikacja (zakres wartości) wskaźnika BMI została opracowana wyłącznie dla dorosłych[3] i nie może być stosowana u dzieci. Dla oceny prawidłowego rozwoju dziecka wykorzystuje się siatki centylowe, które powinny być dostosowane dla danej populacji. " +
                "Oznaczanie wskaźnika masy ciała ma znaczenie w ocenie zagrożenia chorobami związanymi z nadwagą i otyłością, np.cukrzycą, chorobą niedokrwienną serca, miażdżycą.Podwyższona wartość BMI związana jest ze zwiększonym ryzykiem wystąpienia takich chorób.";
            DisplayAlert("Pomiar BMI", information, "OK");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var dates = (sender as MenuItem).CommandParameter as BMI;
            //var rekord = string.Format("Czy na pewno chcesz usunąć rekord {0}?", recipe.Name);
            //var answer = DisplayAlert("Question?", rekord, "Tak", "Nie");
            _conntection.DeleteAsync(dates);
            _dates.Remove(dates);
        }
    }
}