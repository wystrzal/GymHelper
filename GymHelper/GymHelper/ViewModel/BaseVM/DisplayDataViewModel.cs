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
    public abstract class DisplayDataViewModel<TEntity> : ReadDataViewModel<TEntity>
        where TEntity : class
    {
        public abstract ICommand NavigateToAddDataCommand { get; }
        public abstract ICommand NavigateToEditDataCommand { get; }
        public ICommand DeleteDataCommand
            => new Command<TEntity>(async (data) => await DeleteData(data));

        protected virtual async Task DeleteData(TEntity entity) 
        {
            await unitOfWork.Repository<TEntity>().Delete(entity);
            await unitOfWork.SaveChanges();
            await ReadData();
        }
    }
}