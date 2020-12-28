using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.BaseVM;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class NewProductPageVM : AddDataViewModel<Product>
    {
        private readonly NewProductCommand newProductCommand;
        public override BaseCommand AddDataCommand { get { return newProductCommand; } }

        public NewProductPageVM()
        {
            newProductCommand = new NewProductCommand(this);
            product = new Product
            {
                UserId = App.Data.User.UserId
            };
        }

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
                AddDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }

        private float calories;
        public float Calories
        {
            get { return calories; }
            set
            {
                calories = value;
                product.Calories = calories;
                AddDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Calories");
            }
        }

        private int grams;
        public int Grams
        {
            get { return grams; }
            set
            {
                grams = value;
                product.Grams = grams;
                AddDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Grams");
            }
        }

        private float protein;
        public float Protein
        {
            get { return protein; }
            set
            {
                protein = value;
                product.Protein = protein;
                AddDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Protein");
            }
        }

        private float carbohydrates;
        public float Carbohydrates
        {
            get { return carbohydrates; }
            set
            {
                carbohydrates = value;
                product.Carbohydrates = carbohydrates;
                AddDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Carbohydrates");
            }
        }

        private float fat;
        public float Fat
        {
            get { return fat; }
            set
            {
                fat = value;
                product.Fat = fat;
                AddDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Fat");
            }
        }

        public override async Task AddData(Product entity)
        {
            if (await ProductExist(entity))
            {
                await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Istnieje już taki produkt.", "Ok");
                return;
            }

            await base.AddData(entity);
        }

        private async Task<bool> ProductExist(Product product)
        {
            return await unitOfWork.Repository<Exercise>()
                .CheckIfExistByCondition(x => x.Name == product.Name && x.UserId == product.UserId);
        }
    }
}
