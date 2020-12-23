using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.BaseVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class DietPageVM : DisplayDataViewModel<Product>
    {
        public override ICommand NavigateToAddDataCommand => throw new NotImplementedException();
        public override ICommand NavigateToEditDataCommand
            => new Command<Product>(async (product) => await NavigateService.Navigate<EditDietProductPage>(product));

        public override async Task ReadData()
        {
            var products = await unitOfWork.Repository<Product>()
                .ReadAllByCondition(x => x.DietId == App.Data.User.Diet.DietId);

            Collection.FillCollection(products);
        }

        public override async Task DeleteData(Product entity)
        {
            var diet = await unitOfWork
                .Repository<Diet>().ReadFirstByConditionWithInclude(x => x.DietId == App.Data.User.Diet.DietId, y => y.Products);

            diet.Products.Remove(entity);
            await unitOfWork.Repository<Diet>().SaveChanges();

            await ReadData();
        }
    }
}
