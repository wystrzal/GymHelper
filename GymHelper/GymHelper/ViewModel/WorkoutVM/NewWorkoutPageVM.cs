using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.BaseVM;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class NewWorkoutPageVM : AddDataViewModel
    {
        private readonly NewWorkoutCommand newWorkoutCommand;
        public override BaseCommand AddDataCommand { get { return newWorkoutCommand; } }

        public NewWorkoutPageVM()
        {
            newWorkoutCommand = new NewWorkoutCommand(this);
            workout = new Workout { UserId = App.Data.User.UserId };
        }

        private Workout workout;
        public Workout Workout
        {
            get { return workout; }
            set
            {
                workout = value;
                OnPropertyChanged("Workout");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                workout.Name = name.ToLower();
                AddDataCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }
    }
}
