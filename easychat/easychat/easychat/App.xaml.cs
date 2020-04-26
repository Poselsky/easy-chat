using Firebase.Database;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat
{
    public partial class App : Application
    {
        public static MainMasterDetailPage MasterDetailPage { get; private set; }
        public static FirebaseClient FirebaseClient { get; private set; }
        public App()
        {
            InitializeComponent();

            FirebaseClient = new FirebaseClient("https://easychat-3685d.firebaseio.com");


            var main = new MainMasterDetailPage();
            MainPage = main;
            MasterDetailPage = main;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
