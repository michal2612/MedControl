using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;

namespace Praca_Inzynierska
{

    public class Glukoza
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Value { get; set; }
        public string Date { get; set; }
    }
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Glucose : ContentPage
	{
        private SQLiteAsyncConnection _conntection;
        private ObservableCollection<Glukoza> _glucose;

        public Glucose ()
		{
			InitializeComponent ();
            _conntection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        protected override async void OnAppearing()
        {
            await _conntection.CreateTableAsync<Glukoza>();
            var dates = await _conntection.Table<Glukoza>().ToListAsync();
            _glucose = new ObservableCollection<Glukoza>(dates);
            list.ItemsSource = _glucose;
            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var wartosc = Convert.ToInt32(number.Text);
                if (wartosc > 126)
                    DisplayAlert("Wynik", "Taki wynik odnotowany w dwóch pomiarach, to diagnozowana jest cukrzyca. Skontaktuj się z lekarzem.", "OK");
                else if (wartosc > 100 && wartosc <= 125)
                    DisplayAlert("Wynik", "Nieprawidłowy poziom glukozy na czczo(stan przedcukrzycowy). Skontaktuj się z lekarzem.", "OK");
                else if (wartosc >= 70 && wartosc <= 99)
                    DisplayAlert("Wynik", "Poziom glukozy na prawidłowym poziomie.", "OK");
                else
                    DisplayAlert("Wynik", "Wartość nie poprawna! Skontaktuj się z lekarzem!", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Błąd danych", "Wprowadź poprawną wartość poziomu glukozy we krwi!", "OK");
            }
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(number.Text) == 0)
                    await DisplayAlert("Błąd!", "Podana wartość wynosi zero bądź jest nie prawidłowa!", "OK");
                else
                {
                    var date = DateTime.Today.ToString("d");
                    var glucose = new Glukoza { Date = date, Value = Convert.ToInt32(number.Text) };
                    await _conntection.InsertAsync(glucose);
                    _glucose.Add(glucose);
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Błąd danych", "Wprowadź poprawną wartość poziomu glukozy we krwi!", "OK");
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            var wybor = _glucose[0];
            _conntection.DeleteAsync(wybor);
            _glucose.Remove(wybor);
        }

        private void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list.SelectedItem = null;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Poziom glukozy we krwi", "1. Prawidłowy poziom cukru we krwi - normy Glukoza to cukier prosty, który jest niezbędny" +
                " do dostarczania organizmowi energii.Prawidłowy poziom cukru we krwi jest fundamentem zdrowia w przypadku osób chorych na cukrzycę" +
                " - dlatego ważne jest regularne monitorowanie ilości glukozy. Poznaj normy poziomu cukru we krwi i dowiedz się, czy w twoim organizmie" +
                " nie występuje nieprawidłowa ilość, mogąca świadczyć o chorobie. U osób dorosłych glukoza na czczo " +
                "powinna mieścić się w normie, która wynosi poniżej 100 mg / dl, glikemia poposiłkowa powinna wynosić " +
                "poniżej 140 mg / dl.Jest to prawidłowy poziom cukru we krwi. Biorąc pod uwagę te same warunki, u dzieci i " +
                "młodzieży prawidłowy poziom cukru we krwi na czczo wynosi 70 - 110 mg / dl, zaś po spożyciu posiłku 70 - " +
                "140 mg / dl.U osób w podeszłym wieku, diabetyków prawidłowy poziom cukru we krwi na czczo powinien wynosić " +
                "od 80 d0 140 mg / dl, zaś w glikemii poposiłkowej poniżej 180 mg / dl.", "OK");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var dates = (sender as MenuItem).CommandParameter as Glukoza;
            _conntection.DeleteAsync(dates);
            _glucose.Remove(dates);
        }

        private void list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var value = e.Item as Glukoza;
            DisplayAlert("Szczegółowe informacje", "Poziom glukozy: " + value.Value + "\n" + "Data badania: " + value.Date, "OK");
        }
    }
}