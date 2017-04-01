using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace UkrGo
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;
        public App()
        {
            InitializeComponent();
//#if !DEBUG

                FlowListView.Init();
//#endif
            MainPage = GetMainPage();


        }


        public static Page GetMainPage()
        {
          
            return new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
