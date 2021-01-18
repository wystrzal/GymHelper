using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class EditDietProductPageVM : EditDataViewModel<Product>
    {
        public override ICommand EditDataCommand =>
            new Command<Product>(async (product) => await Update(product));
        public ICommand GramsChangedCommand
            => new Command<string>((text) => Product.Grams = (int)RestoreOldValue(text, OldProduct.Grams, Product.Grams));

        public Product OldProduct { get; private set; }

        private Product product;
        public Product Product
        {
            get => product;
            set
            {
                product = value;
                if (OldProduct == null)
                {
                    OldProduct = (Product)Product.Clone();
                }
                OnPropertyChanged("Product");
            }
        }

        protected override async Task Update(Product product)
        {
            var diet = App.Data.User.Diet;
            NutrientsManagement.SubtractNutrients(product, diet);
            ChangeProductParameters(product);
            NutrientsManagement.AddNutrients(product, diet);
            await base.Update(product);
        }

        private void ChangeProductParameters(Product product)
        {
            var caloriesPerGram = product.Calories / OldProduct.Grams;
            var proteinsPerGram = product.Proteins / OldProduct.Grams;
            var fatPerGram = product.Fats / OldProduct.Grams;
            var carbohydratesPerGram = product.Carbohydrates / OldProduct.Grams;

            product.Calories = caloriesPerGram * product.Grams;
            product.Proteins = proteinsPerGram * product.Grams;
            product.Fats = fatPerGram * product.Grams;
            product.Carbohydrates = carbohydratesPerGram * product.Grams;
        }
    }
}