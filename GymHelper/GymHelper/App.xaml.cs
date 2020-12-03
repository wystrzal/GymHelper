using GymHelper.Data;
using GymHelper.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper
{
    public partial class App : Application
    {
        public static DataContext DataContext { get; set; }
        public static User User { get; set; }

        public App(string dbPath)
        {
            InitializeComponent();

            DataContext = new DataContext(dbPath);

            MainPage = new LoginPage();
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
