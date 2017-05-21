using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.Data;
using UkrGo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UkrGo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopicPage : ContentPage
    {
        public TopicPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
            listView.ItemsSource = await App.Database.GetItemsAsync();
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
                BindingContext = new MainPageViewModel() { Url = (e.SelectedItem as Topic).URL }
            });
        }
    }
}
