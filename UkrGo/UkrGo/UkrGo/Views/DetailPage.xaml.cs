﻿using FFImageLoading.Forms;
using Plugin.Messaging;
using System;
using UkrGo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
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

        void OnImageDoubleTap(object sender, EventArgs args)
        {
            var imageSender = (CachedImage)sender;
            Navigation.PushAsync(new ImageViewPage((BindingContext as DetailPageViewModel).Data), true);
        }

        private void OnContactTapped(object sender, EventArgs e)
        {

            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
                phoneCallTask.MakePhoneCall(((BindingContext as DetailPageViewModel).Data).Contact);
        }
    }
}
