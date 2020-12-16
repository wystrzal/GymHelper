using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands.ExerciseCommands
{
    public class EditExerciseCommand : BaseCommand
    {
        private readonly EditExercisePageVM viewModel;

        public EditExerciseCommand(EditExercisePageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
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

        public override async void Execute(object parameter)
        {
            await viewModel.Update((Exercise)parameter);
        }
    }
}
