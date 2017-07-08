using System;
using System.Collections.Generic;
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _topics = await App.Database.GetItemsAsync();
            listView.ItemsSource = _topics;
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTopicPage
           {
                BindingContext = new Topic()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);

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

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

    }
}
