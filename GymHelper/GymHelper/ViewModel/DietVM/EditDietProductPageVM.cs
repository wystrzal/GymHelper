using GymHelper.Data.Interfaces;
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
        public override BaseCommand EditDataCommand => throw new NotImplementedException();

        private Product product;
        public Product Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        private int grams;
        public int Grams
        {
            get { return grams; }
            set
            {
                grams = value;
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Grams");
            }
        }

        public override async Task Update(Product product)
        {
            ChangeProductParameters(product);
            await base.Update(product);
        }

        private void ChangeProductParameters(Product product)
        {
            var caloriesPerGram = product.Calories / product.Grams;
            var proteinsPerGram = product.Protein / product.Grams;
            var fatPerGram = product.Fat / product.Grams;
            var carbohydratesPerGram = product.Carbohydrates / product.Grams;

            product.Calories = caloriesPerGram * Grams;
            product.Protein = proteinsPerGram * Grams;
            product.Fat = fatPerGram * Grams;
            product.Carbohydrates = carbohydratesPerGram * Grams;
            product.Grams = Grams;
        }
    }
}
