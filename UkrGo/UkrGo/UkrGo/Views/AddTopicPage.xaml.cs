using Acr.UserDialogs;
using System;
using UkrGo.Data;
using Xamarin.Forms;

namespace UkrGo.Views
{

    public partial class AddTopicPage : ContentPage
    {
        private string _currentUrl;
        public AddTopicPage()
        {
            InitializeComponent();
            wb.Source = new UrlWebViewSource() { Url = @"http://www.kiev.ukrgo.com" };
            
            NavigationPage.SetHasNavigationBar(this, false);
            fabBtn.IsVisible = false;
        }
        protected override bool OnBackButtonPressed()
        {
            if (wb.CanGoBack)
                wb.GoBack();
            else
                Navigation.PopAsync();
            return true;
        }

        async void Handle_FabClicked(object sender, EventArgs e)
        {
            PromptResult pr = await UserDialogs.Instance.PromptAsync("Название рубрики");
            if (pr.Ok)
                await App.Database.SaveItemAsync(new Topic() { TopicName = pr.Value, URL = _currentUrl });
        }

        private void wb_Navigated(object sender, WebNavigatedEventArgs e)
        {
            _currentUrl = e.Url;
            if (_pd != null)
                _pd.Hide();
            fabBtn.IsVisible = true;


        }

        IProgressDialog _pd;
        private void wb_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (_pd == null)
                _pd = UserDialogs.Instance.Loading("Please wait", null, null, true, MaskType.Clear);
            else _pd.Show();
        }
 
    }
}
