using Login.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login
{
    public partial class App : Application
    {
        public static string HostUrl = "http://192.168.1.14:8000";
        public App()
        {
            InitializeComponent();
            
            //MainPage = new NavigationPage(new Log1());
            //MainPage = new NavigationPage(new Searchdoctor());
            MainPage = new NavigationPage(new autocomplete());
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
