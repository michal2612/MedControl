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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Master : ContentPage
    {
        public ListView ListView;

        public MasterDetailPage1Master()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPage1MasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }
            public ObservableCollection<Page> MenuItems2 { get; set; }

            public MasterDetailPage1MasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                {
                    new MasterDetailPage1MenuItem { Id = 0, Title = "Page 1" },
                    new MasterDetailPage1MenuItem { Id = 1, Title = "Page 2" },
                    new MasterDetailPage1MenuItem { Id = 2, Title = "Page 3" },
                    new MasterDetailPage1MenuItem { Id = 3, Title = "Page 4" },
                    new MasterDetailPage1MenuItem { Id = 4, Title = "Page 5" },
                });
                MenuItems2 = new ObservableCollection<Page>
                {
                    new MasterDetailPage1Detail(),
                    new Med(),
                    new Rehab(),
                    new Measurment(),
                    new Notes(),
                    new Visit()
                };
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}