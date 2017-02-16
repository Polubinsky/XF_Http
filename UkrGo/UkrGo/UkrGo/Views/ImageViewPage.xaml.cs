using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.ViewModel;
using Xamarin.Forms;

namespace UkrGo.Views
{
    public partial class ImageViewPage : ContentPage
    {
        public ImageViewPage(string link)
        {
            InitializeComponent();
            BindingContext = new ImagePageViewModel(link);
            NavigationPage.SetHasNavigationBar(this, false);
        }


    }
}
