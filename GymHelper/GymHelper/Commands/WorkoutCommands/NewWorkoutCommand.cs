using GymHelper.Data.Interfaces;
using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands
{
    public class NewWorkoutCommand : BaseCommand
    {
        private readonly NewWorkoutPageVM viewModel;

        public NewWorkoutCommand(NewWorkoutPageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            var workout = (Workout)parameter;

            if (workout == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(workout.Name))
            {
                return false;
            }

            return true;
        }

        public override async void Execute(object parameter)
        {
            await viewModel.AddData((Workout)parameter);
        }
    }
}
