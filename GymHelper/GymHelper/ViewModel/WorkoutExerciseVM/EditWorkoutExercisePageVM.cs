using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class EditWorkoutExercisePageVM : EditDataViewModel
    {
        public override BaseCommand EditDataCommand => throw new NotImplementedException();

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

        private int series;
        public int Series
        {
            get { return series; }
            set
            {
                series = value;
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Series");
            }
        }

        private int repetition;
        public int Repetition
        {
            get { return repetition; }
            set
            {
                repetition = value;
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Repetition");
            }
        }

        private int weight;
        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                EditDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Weight");
            }
        }
    }
}
