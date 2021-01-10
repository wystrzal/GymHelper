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
using System.Windows.Input;

namespace GymHelper.ViewModel
{
    public class EditExercisePageVM : EditDataViewModel<Exercise>
    {
        private readonly EditExerciseCommand editExerciseCommand;
        public override ICommand EditDataCommand => editExerciseCommand;

        public EditExercisePageVM()
        {
            editExerciseCommand = new EditExerciseCommand(this);
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
                exercise.Name = name.ToLower();
                ((BaseCommand)EditDataCommand).RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }
    }
}
