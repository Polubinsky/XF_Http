using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UkrGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTopicPage : ContentPage
    {
        private string _currentUrl;
        public AddTopicPage()
        {
            InitializeComponent();
            wb.Source = new UrlWebViewSource() { Url = @"http://www.kiev.ukrgo.com" };
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            PromptResult pr = await UserDialogs.Instance.PromptAsync("Название рубрики");
            await App.Database.SaveItemAsync(new Topic() { TopicName = pr.Value, URL = _currentUrl });
        }

        private void wb_Navigated(object sender, WebNavigatedEventArgs e)
        {
            _currentUrl = e.Url;
        }
    }
}
