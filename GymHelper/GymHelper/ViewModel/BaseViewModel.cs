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
        public INavigateService NavigateService { get; private set; }
        public BaseViewModel()
        {
            NavigateService = App.NavigateService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
