using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.WorkoutExerciseCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class EditWorkoutExercisePageVM : EditDataViewModel<WorkoutExercise>
    {
        private readonly EditWorkoutExerciseCommand editWorkoutExerciseCommand;
        public override BaseCommand EditDataCommand { get { return editWorkoutExerciseCommand; } }

        public EditWorkoutExercisePageVM()
        {
            editWorkoutExerciseCommand = new EditWorkoutExerciseCommand(this);
        }

        private WorkoutExercise workoutExercise;
        public WorkoutExercise WorkoutExercise
        {
            get { return workoutExercise; }
            set
            {
                workoutExercise = value;
                OnPropertyChanged("WorkoutExercise");
            }
        }
    }
}