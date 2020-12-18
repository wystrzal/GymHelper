using GymHelper.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public abstract class EditDataViewModel : BaseViewModel
    {
        public virtual async Task Update<TEntity>(TEntity entity) where TEntity : class
        {
            await unitOfWork.Repository<TEntity>().Update(entity);
            await unitOfWork.Repository<TEntity>().SaveChanges();
            await NavigateService.NavigateBack();
        }
    }
}
