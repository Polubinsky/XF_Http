using UkrGo.Model;
using UkrGo.ViewModel;
using Xamarin.Forms;

namespace UkrGo.Views
{
    public partial class ImageViewPage : ContentPage
    {
        public ImageViewPage(DetailData data)
        {
            InitializeComponent();
            BindingContext = new ImagePageViewModel(data);
            NavigationPage.SetHasNavigationBar(this, false);
        }


    }
}
