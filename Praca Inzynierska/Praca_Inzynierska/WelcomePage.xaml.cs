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
	public partial class WelcomePage : ContentPage
	{
		public WelcomePage ()
		{
			InitializeComponent ();
            //image.Source = ImageSource.FromResource("Praca_Inzynierska.Images.background.jpg");
		}
        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationMenu());
        }
    }
}