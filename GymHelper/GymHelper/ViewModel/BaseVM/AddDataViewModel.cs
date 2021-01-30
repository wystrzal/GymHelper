using GymHelper.Data.Interfaces;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class AddDataViewModel<TEntity> : BaseViewModel
        where TEntity : class
    {
        protected IAlertService alertService;
        public abstract ICommand AddDataCommand { get; }

        public AddDataViewModel()
        {
            alertService = App.Data.AlertService;
        }

        public virtual async Task AddData(TEntity entity)
        {
            await unitOfWork.Repository<TEntity>().Add(entity);
            await unitOfWork.SaveChanges();
            await navigateService.NavigateBack();
        }
    }
}