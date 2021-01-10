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

namespace GymHelper.ViewModel
{
    public class EditProductPageVM : EditDataViewModel<Product>
    {
        private readonly EditProductCommand editProductCommand;
        public override ICommand EditDataCommand => editProductCommand;

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
                if (OldProduct == null)
                {
                    OldProduct = (Product)product.Clone();
                }
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
                ((BaseCommand)EditDataCommand).RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
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
