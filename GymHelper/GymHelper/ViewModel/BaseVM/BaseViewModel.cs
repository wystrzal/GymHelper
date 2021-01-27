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
        protected readonly INavigateService navigateService;
        protected readonly IUnitOfWork unitOfWork;

        public BaseViewModel()
        {
            navigateService = App.Data.NavigateService;
            unitOfWork = App.Data.UnitOfWork;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}