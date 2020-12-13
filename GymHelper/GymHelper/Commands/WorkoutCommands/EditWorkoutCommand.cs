using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands
{
    class EditWorkoutCommand : ICommand
    {
        private readonly EditWorkoutPageVM viewModel;

        public EditWorkoutCommand(EditWorkoutPageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
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

        public async void Execute(object parameter)
        {
            await viewModel.Update((Workout)parameter);
        }
    }
}
