using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands.ExerciseCommands
{
    class NewExerciseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly NewExercisePageVM viewModel;

        public NewExerciseCommand(NewExercisePageVM viewModel)
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

        public async void Execute(object parameter)
        {
            await viewModel.AddExercise((Exercise)parameter);
        }
    }
}