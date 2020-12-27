using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands.ExerciseCommands
{
    public class EditProductCommand : BaseCommand
    {
        private readonly EditProductPageVM viewModel;

        public EditProductCommand(EditProductPageVM viewModel)
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

            if (string.IsNullOrEmpty(product.Name))
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
