using RealWorldApp.Pages;
using RealWorldApp.Services;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignupPage());
            //var accesstoken = Preferences.Get("accessToken", string.Empty);
            //if (string.IsNullOrEmpty(accesstoken))
            //{
            //    MainPage = new NavigationPage(new SignupPage());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new HomePage());
            //}


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
