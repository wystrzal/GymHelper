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
        public static DataContext DataContext { get; set; }
        public static User User { get; set; }
        public static IAuthService AuthService { get; set; }
        public static IUnitOfWork UnitOfWork { get; set; }
        public static INavigateService NavigateService { get; set; }

        public App(string dbPath)
        {
            InitializeComponent();

            DataContext = new DataContext(dbPath);
            ProvideServices();

            MainPage = new NavigationPage(new LoginPage());
        }

        private static void ProvideServices()
        {
            var serviceProvider = AppServiceProvider.BuildServiceProvider();
            AuthService = serviceProvider.GetRequiredService<IAuthService>();
            UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            NavigateService = serviceProvider.GetRequiredService<INavigateService>();
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
