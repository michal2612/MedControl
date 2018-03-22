using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praca_Inzynierska
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationMenu : ContentPage
	{
        public NavigationMenu()
        {
            InitializeComponent();

            var names = new List<string>
            {
                "Keki",
                "SDDS"
            };
            //Lista.ItemsSource = names;
		}
        async private void move_Med(object sender, EventArgs e) //przenosi do strony z lekaami
        {
            await Navigation.PushAsync(new Med());
        }
        async private void move_Measurment(object sender, EventArgs e) //przenosi do strony z pomiarami
        {
            await Navigation.PushAsync(new Measurment());
        }
        async private void move_Notes(object sender, EventArgs e) //przenosi do strony z notatkami
        {
            await Navigation.PushAsync(new Notes());
        }
        async private void move_Visit(object sender, EventArgs e) //przenosi do strony z wizytami
        {
            await Navigation.PushAsync(new Visit());
        }
        async private void move_Rehab(object sender, EventArgs e) //przenosi do strony z wizytami
        {
            await Navigation.PushAsync(new Rehab());
        }
    }
}