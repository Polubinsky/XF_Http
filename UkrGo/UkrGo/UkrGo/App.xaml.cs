using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using UkrGo.Data;
using UkrGo.Interfaces;
using UkrGo.Views;


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
//#if !DEBUG

                FlowListView.Init();
//#endif
            MainPage = GetMainPage();


        }


        public static Page GetMainPage()
        {
          
            return new NavigationPage(new TopicPage());
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
