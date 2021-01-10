﻿using GymHelper.Data.Interfaces;
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