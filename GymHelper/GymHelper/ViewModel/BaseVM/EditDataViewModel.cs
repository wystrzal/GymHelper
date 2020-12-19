using GymHelper.Data.Interfaces;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public abstract class EditDataViewModel : BaseViewModel
    {
        public abstract BaseCommand EditDataCommand { get; }

        public virtual async Task Update<TEntity>(TEntity entity) where TEntity : class
        {
            await unitOfWork.Repository<TEntity>().Update(entity);
            await unitOfWork.Repository<TEntity>().SaveChanges();
            await NavigateService.NavigateBack();
        }
    }
}
