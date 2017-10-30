using UkrGo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UkrGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPageView : ContentPage
    {
        public SettingsPageView()
        {
            InitializeComponent();
            BindingContext = new SettingsPageViewModel();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}