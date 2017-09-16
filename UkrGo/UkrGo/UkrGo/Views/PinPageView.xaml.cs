using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UkrGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinPageView : ContentPage
    {
        public PinPageView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new PinPageViewModel();
        }
    }
}