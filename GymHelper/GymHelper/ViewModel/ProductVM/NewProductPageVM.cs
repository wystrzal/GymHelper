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
using System.Windows.Input;

namespace GymHelper.ViewModel
{
    public class NewProductPageVM : AddDataViewModel<Product>
    {
        private readonly NewProductCommand newProductCommand;
        public override ICommand AddDataCommand => newProductCommand;

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
            get => product;
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                product.Name = name.ToLower();
                ((BaseCommand)AddDataCommand).RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }

        public override async Task AddData(Product entity)
        {
            if (await ProductExist(entity))
            {
                await alertService.DisplayAlert("Niepowodzenie", "Istnieje już taki produkt.", "Ok");
                return;
            }

            await base.AddData(entity);
        }

        private async Task<bool> ProductExist(Product product)
        {
            return await unitOfWork.Repository<Product>()
                .CheckIfExistByCondition(x => x.Name == product.Name && x.UserId == product.UserId);
        }
    }
}