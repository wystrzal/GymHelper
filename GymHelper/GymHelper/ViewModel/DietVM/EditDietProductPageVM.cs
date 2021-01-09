using GymHelper.Commands.DietCommand;
using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.WorkoutExerciseCommands;
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
        private readonly EditDietProductCommand editDietProductCommand;
        public override BaseCommand EditDataCommand { get { return editDietProductCommand; } }

        public EditDietProductPageVM()
        {
            editDietProductCommand = new EditDietProductCommand(this);
        }

        private Product OldProduct;

        private Product product;
        public Product Product
        {
            get { return product; }
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

        public override async Task Update(Product product)
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
