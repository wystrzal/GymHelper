using GymHelper.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class DisplayDataViewModel : BaseViewModel
    {
        public abstract Task ReadData();

        public virtual async Task DeleteData<TEntity>(TEntity entity) 
            where TEntity : class
        {
            await unitOfWork.Repository<TEntity>().Delete(entity);
            await unitOfWork.Repository<TEntity>().SaveChanges();
            await ReadData();
        }
    }
}
