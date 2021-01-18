using GymHelper.Data.Interfaces;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymHelper.ViewModel
{
    public abstract class EditDataViewModel<TEntity> : BaseViewModel
        where TEntity : class
    {
        public abstract ICommand EditDataCommand { get; }

        protected virtual async Task Update(TEntity entity)
        {
            await unitOfWork.Repository<TEntity>().Update(entity);
            await unitOfWork.SaveChanges();
            await NavigateService.NavigateBack();
        }

        protected object RestoreOldValue(string text, object oldValue, object currentValue)
        {
            if (string.IsNullOrWhiteSpace(text) && oldValue.GetType() == currentValue.GetType())
            {
                return oldValue;
            }

            return currentValue;
        }
    }
}