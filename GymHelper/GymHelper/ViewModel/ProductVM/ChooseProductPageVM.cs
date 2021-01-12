using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers;
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
        private readonly Diet diet = App.Data.User.Diet;
        public override ICommand NavigateToAddDataCommand 
            => new Command(async () => await NavigateService.Navigate<NewProductPage>());
        public override ICommand NavigateToEditDataCommand 
            => new Command<Product>(async (product) => await NavigateService.Navigate<EditProductPage>(product));

        public override async Task ReadData()
        {
            var products = await unitOfWork.Repository<Product>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);

            Collection.FillCollection(products);
        }

        public override async Task DeleteData(Product entity)
        {
            if (await ProductExistInDiet(entity, diet))
            {
                NutrientsManagement.SubtractNutrients(entity, diet);
            }

            await base.DeleteData(entity);
        }

        protected override async Task AddSelectedData()
        {
            await SelectedData.LoopAsync(AddProductToDiet);
            await NavigateService.NavigateBack();
        }

        private async Task AddProductToDiet(Product product)
        {          
            if (!await ProductExistInDiet(product, diet))
            {
                product.DietId = diet.DietId;
                NutrientsManagement.AddNutrients(product, diet);
                await unitOfWork.SaveChanges();
            }
        }

        private async Task<bool> ProductExistInDiet(Product product, Diet diet)
        {
            return await unitOfWork.Repository<Product>()
                .CheckIfExistByCondition(x => x.DietId == diet.DietId && x.ProductId == product.ProductId);
        }
    }
}
