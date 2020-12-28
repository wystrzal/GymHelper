using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Extensions;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.View.ProductView;
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
    public class ChooseProductPageVM : ChooseDataViewModel<Product>
    {
        public override ICommand NavigateToAddDataCommand 
            => new Command(async () => await NavigateService.Navigate<NewProductPage>());
        public override ICommand NavigateToEditDataCommand 
            => new Command<Product>(async (product) => await NavigateService.Navigate<EditProductPage>(product));

        public override async Task ReadData()
        {
            var products = await unitOfWork.Repository<Product>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);

            Collection.FillCollection(products);
        }

        protected override async Task AddSelectedData()
        {
            List<Task> tasks = new List<Task>();

            foreach (var item in SelectedData)
            {
                tasks.Add(AddProductToDiet(item));
            }

            await Task.WhenAll(tasks);

            await NavigateService.NavigateBack();
        }

        private async Task AddProductToDiet(Product product)
        {
            var diet = await unitOfWork.Repository<Diet>().ReadFirstByCondition(x => x.UserId == App.Data.User.UserId);
            
            if (!await ProductExistInDiet(product, diet))
            {
                product.DietId = diet.DietId;
                await unitOfWork.Repository<Diet>().SaveChanges();
            }
        }

        private async Task<bool> ProductExistInDiet(Product product, Diet diet)
        {
            return await unitOfWork.Repository<Product>()
                .CheckIfExistByCondition(x => x.DietId == diet.DietId && x.ProductId == product.ProductId);
        }
    }
}
