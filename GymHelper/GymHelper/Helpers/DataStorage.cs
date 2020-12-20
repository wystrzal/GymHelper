using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Helpers
{
    public class DataStorage
    {
        public User User { get; set; }
        public Workout Workout { get; set; }
        public DataContext DataContext { get; set; }
        public IAuthService AuthService { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public INavigateService NavigateService { get; set; }
        public IAlertService AlertService { get; set; }

        public DataStorage(string dbPath)
        {
            DataContext = new DataContext(dbPath);
            ProvideServices();
        }

        private void ProvideServices()
        {
            var serviceProvider = AppServiceProvider.BuildServiceProvider();
            AuthService = serviceProvider.GetRequiredService<IAuthService>();
            UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            NavigateService = serviceProvider.GetRequiredService<INavigateService>();
            AlertService = serviceProvider.GetRequiredService<IAlertService>();
        }
    }
}
