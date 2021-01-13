using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class ReadDataViewModel<TEntity> : BaseViewModel
        where TEntity : class
    {
        public ObservableCollection<TEntity> Collection { get; }
        public ICommand PerformSearchCommand
            => new Command<string>(async (query) => await SearchData(query));

        public ReadDataViewModel()
        {
            Collection = new ObservableCollection<TEntity>();
        }

        public abstract Task ReadData();

        public abstract Task SearchData(string query);
    }
}
