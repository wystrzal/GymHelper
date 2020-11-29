using GymHelper.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper
{
    public partial class App : Application
    {
        public static DataContext DataContext { get; set; }

        public App(string dbPath)
        {
            InitializeComponent();

            DataContext = new DataContext(dbPath);

            MainPage = new MainPage();
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
