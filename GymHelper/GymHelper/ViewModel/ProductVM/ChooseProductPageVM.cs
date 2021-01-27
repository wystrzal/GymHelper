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
            => new Command(async () => await navigateService.Navigate<NewProductPage>());
        public override ICommand NavigateToEditDataCommand
            => new Command<Product>(async (product) => await navigateService.Navigate<EditProductPage>(product));

        protected override async Task DeleteData(Product entity)
        {
            if (await ProductExistInDiet(entity, diet))
            {
                NutrientsManagement.SubtractNutrients(entity, diet);
            }

            await base.DeleteData(entity);
        }

        protected override async Task<int> GetDataCount()
        {
            return await unitOfWork.Repository<Product>().ReadDataCount(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query));
        }

        protected override async Task<IEnumerable<Product>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<Product>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query), pageSize, pageIndex * pageSize);
        }

        protected override async Task AddSelectedData()
        {
            await SelectedData.LoopAsync(AddProductToDiet);
            await navigateService.NavigateBack();
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