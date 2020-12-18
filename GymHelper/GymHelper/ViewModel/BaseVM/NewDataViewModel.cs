using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class NewDataViewModel : BaseViewModel
    {
        public virtual async Task AddData<TEntity>(TEntity entity) where TEntity : class
        {
            await unitOfWork.Repository<TEntity>().Add(entity);
            await unitOfWork.Repository<TEntity>().SaveChanges();
            await NavigateService.NavigateBack();
        }
    }
}
