using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
//using Android.App;

namespace Praca_Inzynierska
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRun : ContentPage
    {
        public SQLiteAsyncConnection _connection;

        public AddRun()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            var RunObject = new RunningClass()
            {
                Distance = float.Parse(DistanceEntry.Text),
                Time = SecondTime.Time - FirstTime.Time,
                TodayData = DateTime.Today.ToString("d"),
                TimeStart = FirstTime.Time,
                TimeStop = SecondTime.Time
            };

            await _connection.InsertAsync(RunObject);
            await Navigation.PopAsync();
        }
    }
}