using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class EditProductPageVM : EditDataViewModel<Product>
    {
        public override ICommand EditDataCommand
            => new Command<Product>(async (product) => await Update(product));
        public ICommand NameChangedCommand
            => new Command<string>((text) => Product.Name = (string)RestoreOldValue(text, OldProduct.Name, Product.Name));
        public ICommand GramsChangedCommand
            => new Command<string>((text) => Product.Grams = (int)RestoreOldValue(text, OldProduct.Grams, Product.Grams));
        public ICommand CaloriesChangedCommand
            => new Command<string>((text) => Product.Calories = (float)RestoreOldValue(text, OldProduct.Calories, Product.Calories));
        public ICommand ProteinsChangedCommand
            => new Command<string>((text) => Product.Proteins = (float)RestoreOldValue(text, OldProduct.Proteins, Product.Proteins));
        public ICommand CarbohydratesChangedCommand
            => new Command<string>((text) => Product.Carbohydrates = (float)RestoreOldValue(text, OldProduct.Carbohydrates, Product.Carbohydrates));
        public ICommand FatsChangedCommand
            => new Command<string>((text) => Product.Fats = (float)RestoreOldValue(text, OldProduct.Fats, Product.Fats));

        public Product OldProduct { get; set; }

        private Product product;
        public Product Product
        {
            get => product;
            set
            {
                product = value;
                if (OldProduct == null)
                {
                    OldProduct = (Product)product.Clone();
                }
                OnPropertyChanged("Product");
            }
        }

        public override async Task Update(Product entity)
        {
            entity.Name = entity.Name.ToLower();
            var diet = App.Data.User.Diet;

            if (await ProductExistInDiet(entity, diet))
            {
                NutrientsManagement.SubtractNutrients(OldProduct, diet);
                NutrientsManagement.AddNutrients(entity, diet);
            }

            await base.Update(entity);
        }

        private async Task<bool> ProductExistInDiet(Product product, Diet diet)
        {
            return await unitOfWork.Repository<Product>()
                .CheckIfExistByCondition(x => x.DietId == diet.DietId && x.ProductId == product.ProductId);
        }
    }
}