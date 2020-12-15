﻿using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GymHelper.ViewModel
{
    class EditExercisePageVM : BaseViewModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditExercisePageVM()
        {
            unitOfWork = App.Data.UnitOfWork;
        }

        private Exercise exercise;
        public Exercise Exercise
        {
            get { return exercise; }
            set
            {
                exercise = value;
                OnPropertyChanged("Exercise");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public async void Update(Exercise exercise)
        {
            exercise.Name = Name;
            await unitOfWork.Repository<Exercise>().Update(exercise);
            await NavigateService.NavigateBack();
        }
    }
}
