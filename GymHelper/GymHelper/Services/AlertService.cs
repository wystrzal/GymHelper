using GymHelper.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Services
{
    public class AlertService : IAlertService
    {
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
