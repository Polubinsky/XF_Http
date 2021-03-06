﻿using UkrGo.Model;
using UkrGo.ViewModel;
using UkrGo.Views;
using Xamarin.Forms;

namespace UkrGo
{
    public partial class MainPage : ContentPage
    {
     
        public MainPage()
        {
            InitializeComponent();
           
           // BindingContext = new MainPageViewModel(st);
            flvControl.FlowItemTapped += FlvControlOnFlowItemTapped;
            
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private void FlvControlOnFlowItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var rows = ((MainPageViewModel) BindingContext).Rows;

            RowData rd = itemTappedEventArgs.Item as RowData;
            if (rd == rows[rows.Count - 1])
            {
                ((MainPageViewModel) BindingContext).NextDataCommand.Execute(null);
                return;
            }
            
            if (rd != null)
                Navigation.PushAsync(new DetailPage(rd.Link), true);
        }
    }
}
