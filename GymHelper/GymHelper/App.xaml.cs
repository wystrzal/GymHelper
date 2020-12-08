using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using GymHelper.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper
{
    public partial class App : Application
    {
        public static DataStorage Data { get; set; }

        public App(string dbPath)
        {
            InitializeComponent();
            Data = new DataStorage(dbPath);
            MainPage = new NavigationPage(new LoginPage());
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
