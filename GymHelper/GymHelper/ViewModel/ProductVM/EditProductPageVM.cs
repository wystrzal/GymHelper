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

namespace GymHelper.ViewModel
{
    public class EditProductPageVM : EditDataViewModel<Product>
    {
        private readonly EditProductCommand editProductCommand;
        public override BaseCommand EditDataCommand { get { return editProductCommand; } }

        public EditProductPageVM()
        {
            editProductCommand = new EditProductCommand(this);
        }

        public Product OldProduct { get; set; }

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

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                product.Name = name;
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }

        private int grams;
        public int Grams
        {
            get { return grams; }
            set
            {
                grams = value;
                if (product != null)
                {
                    product.Grams = grams;
                }
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Grams");
            }
        }

        private float calories;
        public float Calories
        {
            get { return calories; }
            set
            {
                calories = value;
                if (product != null)
                {
                    product.Calories = calories;
                }
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Calories");
            }
        }

        private float carbohydrates;
        public float Carbohydrates
        {
            get { return carbohydrates; }
            set
            {
                carbohydrates = value;
                if (product != null)
                {
                    product.Carbohydrates = carbohydrates;
                }
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Carbohydrates");
            }
        }

        private float proteins;
        public float Proteins
        {
            get { return proteins; }
            set
            {
                proteins = value;
                if (product != null)
                {
                    product.Proteins = proteins;
                }
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Proteins");
            }
        }

        private float fats;
        public float Fats
        {
            get { return fats; }
            set
            {
                fats = value;
                if (product != null)
                {
                    product.Fats = fats;
                }
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Fats");
            }
        }

        public override async Task Update(Product entity)
        {
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
