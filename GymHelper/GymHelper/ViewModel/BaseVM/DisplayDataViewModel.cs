using GymHelper.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class DisplayDataViewModel<TEntity> : BaseViewModel
        where TEntity : class
    {
        public ObservableCollection<TEntity> Collection { get; }
        public abstract ICommand NavigateToAddDataCommand { get; }
        public abstract ICommand NavigateToEditDataCommand { get; }
        public ICommand DeleteDataCommand
            => new Command<TEntity>(async (data) => await DeleteData(data));

        public DisplayDataViewModel()
        {
            Collection = new ObservableCollection<TEntity>();
        }

        public abstract Task ReadData();
        public virtual async Task DeleteData(TEntity entity) 
        {
            await unitOfWork.Repository<TEntity>().Delete(entity);
            await unitOfWork.Repository<TEntity>().SaveChanges();
            await ReadData();
        }
    }
}
