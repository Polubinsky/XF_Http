using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.Model;
using Xamarin.Forms;

namespace UkrGo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            IsLoading = true;
            Task.Run(async () =>
            {
                Rows = await WebManager.LoadItemsAsync();
               
                AdsView.IsVisible = true;
                IsLoading = false;
            }).ConfigureAwait(false);
        }


        public bool IsLoading { get; set; }
        IList<RowData> Rows { get; set; } 
    }
}
