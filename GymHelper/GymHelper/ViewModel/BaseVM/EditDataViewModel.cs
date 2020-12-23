using GymHelper.Data.Interfaces;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public abstract class EditDataViewModel<TEntity> : BaseViewModel
        where TEntity : class
    {
        public abstract BaseCommand EditDataCommand { get; }

        public virtual async Task Update(TEntity entity)
        {
            await unitOfWork.Repository<TEntity>().Update(entity);
            await unitOfWork.Repository<TEntity>().SaveChanges();
            await NavigateService.NavigateBack();
        }
    }
}
