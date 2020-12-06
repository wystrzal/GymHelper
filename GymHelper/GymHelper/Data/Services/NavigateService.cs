using GymHelper.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GymHelper.Data.Services
{
    public class NavigateService : INavigateService
    {
        public async Task Navigate<TPage>()
            where TPage : Page
        {
            await App.Current.MainPage.Navigation.PushAsync(Activator.CreateInstance(typeof(TPage)) as Page);
        }

        public async Task Navigate<TPage>(object providedObject)
           where TPage : Page
        {
            await App.Current.MainPage.Navigation.PushAsync(Activator.CreateInstance(typeof(TPage), providedObject) as Page);
        }

        public async Task NavigateBack()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
