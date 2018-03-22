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
	public partial class Measurment : TabbedPage
	{
		public Measurment ()
		{
			InitializeComponent ();
            var pomiary = new List<string> { "Waga", "Puls", "Poziom cukru", "Temperatura", "Dystans[km]", "Kalorie" };
        }
    }
}