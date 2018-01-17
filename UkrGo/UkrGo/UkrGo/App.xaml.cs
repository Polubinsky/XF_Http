using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using UkrGo.Data;
using UkrGo.Interfaces;
using UkrGo.Views;
using UkrGo.Helpers;


namespace UkrGo
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;

        static LinkItemDatabase database;

        public static LinkItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new LinkItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("LinkSQLite.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();
            FlowListView.Init();
           
        }

        private static Page _currentPage;
        public static Page Page
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        public Page GetMainPage()
        {
            //Page = new NavigationPage(new SettingsPageView());
            Page = new NavigationPage(new PinPageView());
            return Page;
        }

		protected override void OnStart()
		{


            if (Settings.AskPinCode)
				MainPage = GetMainPage();
			else MainPage = new NavigationPage(new TopicPage());
		}

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            if (Settings.AskPinCode)
                MainPage = GetMainPage();
        }
    }
}
