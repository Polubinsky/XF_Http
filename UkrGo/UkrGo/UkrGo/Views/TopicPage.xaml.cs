using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UkrGo.Data;
using UkrGo.ViewModel;
using Xamarin.Forms;

namespace UkrGo.Views
{

    public partial class TopicPage : ContentPage
    {
        IList<Topic> _topics;
        public TopicPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await RefreshList();
        }

        private async Task RefreshList()
        {
            _topics = await App.Database.GetItemsAsync();
            listView.ItemsSource = _topics;
        }
        async void Handle_FabClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddTopicPage
            {
                BindingContext = new Topic()
            });
        }
       
        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new MainPage
            {
                BindingContext = new MainPageViewModel((e.SelectedItem as Topic).URL)
            });
        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            await App.Database.DeleteItemAsync(mi.CommandParameter as Topic);
            await RefreshList();
        }

    }
}
