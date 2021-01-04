using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class ReadDataViewModel<TEntity> : BaseViewModel
        where TEntity : class
    {
        public ObservableCollection<TEntity> Collection { get; }

        public ReadDataViewModel()
        {
            Collection = new ObservableCollection<TEntity>();
        }

        public abstract Task ReadData();
    }
}
