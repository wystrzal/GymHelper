using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel.BaseVM
{
    public abstract class ChooseDataViewModel<TEntity> : DisplayDataViewModel<TEntity>
        where TEntity : class
    {
        public List<TEntity> SelectedData;
        public ICommand AddSelectedDataCommand
            => new Command(async () => await AddSelectedData());

        public ChooseDataViewModel()
        {
            SelectedData = new List<TEntity>();
        }

        protected abstract Task AddSelectedData();
    }
}
