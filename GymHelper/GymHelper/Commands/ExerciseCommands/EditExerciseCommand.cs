using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands.ExerciseCommands
{
    class EditExerciseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly EditExercisePageVM viewModel;

        public EditExerciseCommand(EditExercisePageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            var exercise = (Exercise)parameter;

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

        public void Execute(object parameter)
        {
            viewModel.Update((Exercise)parameter);
        }
    }
}
