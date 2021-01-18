using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class EditWorkoutExercisePageVM : EditDataViewModel<WorkoutExercise>
    {
        public override ICommand EditDataCommand =>
            new Command<WorkoutExercise>(async (workoutExercise) => await Update(workoutExercise));
        public ICommand SeriesChangedCommand
            => new Command<string>((text) => WorkoutExercise.Series = (int)RestoreOldValue(text, OldWorkoutExercise.Series, WorkoutExercise.Series));
        public ICommand RepetitionChangedCommand
            => new Command<string>((text) => WorkoutExercise.Repetition = (int)RestoreOldValue(text, OldWorkoutExercise.Repetition, WorkoutExercise.Repetition));
        public ICommand WeightChangedCommand
            => new Command<string>((text) => WorkoutExercise.Weight = (int)RestoreOldValue(text, OldWorkoutExercise.Weight, WorkoutExercise.Weight));

        public WorkoutExercise OldWorkoutExercise { get; private set; }

        private WorkoutExercise workoutExercise;
        public WorkoutExercise WorkoutExercise
        {
            get => workoutExercise;
            set
            {
                workoutExercise = value;
                if (OldWorkoutExercise == null)
                {
                    OldWorkoutExercise = (WorkoutExercise)workoutExercise.Clone();
                }
                OnPropertyChanged("WorkoutExercise");
            }
        }
    }
}