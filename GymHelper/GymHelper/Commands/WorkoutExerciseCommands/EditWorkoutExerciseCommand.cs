using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands.WorkoutExerciseCommands
{
    public class EditWorkoutExerciseCommand : BaseCommand
    {
        private readonly EditWorkoutExercisePageVM viewModel;

        public EditWorkoutExerciseCommand(EditWorkoutExercisePageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            var workoutExercise = (WorkoutExercise)parameter;

            if (workoutExercise == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(workoutExercise.Series.ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(workoutExercise.Repetition.ToString()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(workoutExercise.Weight.ToString()))
            {
                return false;
            }

            return true;
        }

        public override async void Execute(object parameter)
        {
            await viewModel.Update((WorkoutExercise)parameter);
        }
    }
}
