using GymHelper.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
