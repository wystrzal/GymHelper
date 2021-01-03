using GymHelper.Data.Interfaces;
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
    public abstract class DisplayDataViewModel<TEntity> : BaseViewModel, IAsyncInitialization
        where TEntity : class
    {
        public ObservableCollection<TEntity> Collection { get; }
        public abstract ICommand NavigateToAddDataCommand { get; }
        public abstract ICommand NavigateToEditDataCommand { get; }
        public ICommand DeleteDataCommand
            => new Command<TEntity>(async (data) => await DeleteData(data));
        public Task Initialization { get; private set; }

        public DisplayDataViewModel()
        {
            Collection = new ObservableCollection<TEntity>();
            Initialization = ReadData();
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
