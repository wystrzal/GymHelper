using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class EditExercisePageVM : EditDataViewModel
    {
        public BaseCommand EditExerciseCommand { get; private set; }

        public EditExercisePageVM()
        {
            EditExerciseCommand = new EditExerciseCommand(this);
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
                exercise.Name = name;
                EditExerciseCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }
    }
}
