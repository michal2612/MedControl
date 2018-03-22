using Microcharts;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace Praca_Inzynierska
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail : ContentPage
    {
        private SQLiteAsyncConnection _connection;

        public MasterDetailPage1Detail()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            Baza();
            stack.BackgroundColor = Color.FromHex("#1FAECE");

            //var norma = (_connection.QueryAsync<int>("SELECT Value FROM Glukoza", list).ConfigureAwait(false))
            List<int> maslo = new List<int> { 72, 79, 82, 75, 78, 74, 85, 83, 78, 73,82,71,70,76,81};
            List<Entry> entries2 = new List<Entry>();
            foreach(var number in maslo)
            {
                string color;
                if (number < 80)
                    color = "#1faece";
                else
                    color = "#F17A0A";

                var zmienna = new Entry(number)
                {
                    Color = SKColor.Parse(color),
                    Label = Convert.ToString("22-01-2017"),
                    ValueLabel = Convert.ToString(number),
                };
                entries2.Add(zmienna);
            }

            myChart.Chart = new LineChart() { Entries = entries2, PointMode= PointMode.Square, AnimationDuration = TimeSpan.FromSeconds(10) };

            List<float> maslo2 = new List<float> { 2.3f, 3.3f, 2.5f,0f, 2.4f,2.1f,2.8f,2.7f,2.1f,2.9f,1.2f,3.1f,2.5f,2.3f };
            List<Entry> entries3 = new List<Entry>();

            foreach (var number in maslo2)
            {
                string color;
                if (number < 3)
                    color = "#1faece";
                else
                    color = "#F17A0A";

                var zmienna = new Entry(number)
                {
                    Color = SKColor.Parse(color),
                    Label = Convert.ToString("22-01-2017"),
                    ValueLabel = Convert.ToString(number),
                };
                entries3.Add(zmienna);
            }

            myChart2.Chart = new BarChart() { Entries = entries3, AnimationDuration = TimeSpan.FromSeconds(10) };

            List<int> maslo3 = new List<int> { 2950, 3121, 2920, 3100, 2850, 2912, 2750, 3100, 3060, 2750, 2950};
            List<Entry> entries4 = new List<Entry>();

            foreach (var number in maslo3)
            {
                string color;
                if (number < 3000)
                    color = "#1faece";
                else
                    color = "#F17A0A";

                var zmienna = new Entry(number)
                {
                    Color = SKColor.Parse(color),
                    Label = Convert.ToString("22-01-2017"),
                    ValueLabel = Convert.ToString(number),
                };
                entries4.Add(zmienna);
            }

            myChart3.Chart = new RadarChart() { Entries= entries4, AnimationProgress = 12.2F, AnimationDuration = TimeSpan.FromSeconds(10) };

            List<float> maslo4 = new List<float> { 21.5f, 22.0f, 20.3f,22.1f,22.5f,21.9f,21.6f, 22.0f,20.9f };
            List<Entry> entries5 = new List<Entry>();

            foreach (var number in maslo4)
            {
                string color;
                if (number < 3000)
                    color = "#1faece";
                else
                    color = "#F17A0A";

                var zmienna = new Entry(number)
                {
                    Color = SKColor.Parse(color),
                    Label = Convert.ToString("22-01-2017"),
                    ValueLabel = Convert.ToString(number),
                };
                entries5.Add(zmienna);
            }

            myChart5.Chart = new BarChart() { Entries = entries5, AnimationDuration = TimeSpan.FromSeconds(10) };
        }

        public void DoInzynierki()
        {
            var list = new List<int>();
            var DbProperties = (_connection.QueryAsync<int>("SELECT Value FROM Glukoza LIMIT 10", list).ConfigureAwait(false));
            List<Entry> entries = new List<Entry>();

            foreach(var number in list)
            {
                string color;
                if (number < 99)
                    color = "#1faece";
                else
                    color = "#F17A0A";

                var newEntries = new Entry(number)
                {
                    Color = SKColor.Parse(color),
                    ValueLabel = Convert.ToString(number),
                };
                entries.Add(newEntries);
            }

            myChart.Chart = new LineChart()
            {
                Entries = entries,
                PointMode = PointMode.Square,
                AnimationDuration = TimeSpan.FromSeconds(5)
            };
        }
        public void Baza()
        {
            List<int> list = new List<int>();
            //var norma = (await _connection.QueryAsync<int>("SELECT Value FROM Glukoza", list)).ToList();
            //listaa.ItemsSource = list;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}