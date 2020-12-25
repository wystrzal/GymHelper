using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
                product.Grams = grams;
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
                product.Calories = calories;
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
                product.Carbohydrates = carbohydrates;
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Carbohydrates");
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
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Protein");
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
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Fat");
            }
        }

    }
}
