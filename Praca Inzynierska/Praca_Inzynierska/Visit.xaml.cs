using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praca_Inzynierska
{
    public class CalendaryDates
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public Color EventColor { get; set; }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Visit : ContentPage
	{
		public Visit ()
		{
			InitializeComponent ();
            Kalendarz.SpecialDates.Add(new XamForms.Controls.SpecialDate(DateTime.Now.AddDays(5)) { BackgroundColor = Color.Red });
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
        }

        private void Kalendarz_DateClicked(object sender, XamForms.Controls.DateTimeEventArgs e)
        {
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddDate());
        }
    }
}