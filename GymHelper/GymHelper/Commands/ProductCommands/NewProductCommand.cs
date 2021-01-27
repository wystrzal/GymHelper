using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands.ExerciseCommands
{
    public class NewProductCommand : BaseCommand
    {
        private readonly NewProductPageVM viewModel;

        public NewProductCommand(NewProductPageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            var exercise = (Product)parameter;

            if (exercise == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(exercise.Name))
            {
                return false;
            }

            return true;
        }

        public override async void Execute(object parameter)
        {
            await viewModel.AddData((Product)parameter);
        }
    }
}