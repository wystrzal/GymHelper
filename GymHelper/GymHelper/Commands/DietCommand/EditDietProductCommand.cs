using GymHelper.Models;
using GymHelper.ViewModel;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Commands.DietCommand
{
    public class EditDietProductCommand : BaseCommand
    {
        private readonly EditDietProductPageVM viewModel;

        public EditDietProductCommand(EditDietProductPageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            var product = (Product)parameter;

            if (product == null)
            {
                return false;
            }

            if (product.Grams <= 0)
            {
                return false;
            }

            return true;
        }

        public override async void Execute(object parameter)
        {
            await viewModel.Update((Product)parameter);
        }
    }
}
