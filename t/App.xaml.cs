using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace t
{
    public partial class App : Application
    {

        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new SessionDataPage());

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new t.MainPage());
            }

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
