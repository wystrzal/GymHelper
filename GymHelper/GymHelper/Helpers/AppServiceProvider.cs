using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Helpers
{
    public static class AppServiceProvider
    {
        public static ServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddSingleton<IUnitOfWork, UnitOfWork>()
                .AddSingleton<IAuthService, AuthService>()
                .AddSingleton<INavigateService, NavigateService>()
                .AddSingleton<IAlertService, AlertService>()
                .BuildServiceProvider();
        }
    }
}
