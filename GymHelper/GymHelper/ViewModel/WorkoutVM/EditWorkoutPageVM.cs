using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class EditWorkoutPageVM : EditDataViewModel
    {
        public BaseCommand EditWorkoutCommand { get; private set; }

        public EditWorkoutPageVM()
        {
            EditWorkoutCommand = new EditWorkoutCommand(this);
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
                workout.Name = name;
                EditWorkoutCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }
    }
}
