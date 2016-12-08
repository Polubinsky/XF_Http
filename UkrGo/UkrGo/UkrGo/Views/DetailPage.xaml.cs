using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.ViewModel;
using Xamarin.Forms;

namespace UkrGo.Views
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(string link)
        {
            InitializeComponent();
            BindingContext = new DetailPageViewModel(link);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
