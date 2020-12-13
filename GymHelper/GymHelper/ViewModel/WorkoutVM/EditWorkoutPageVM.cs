using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    class EditWorkoutPageVM : BaseViewModel
    {
        private readonly IUnitOfWork unitOfWork;
        public EditWorkoutPageVM()
        {
            unitOfWork = App.Data.UnitOfWork;
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
                OnPropertyChanged("Name");
            }
        }

        public async void Update(Workout workout)
        {
            workout.Name = Name;
            await unitOfWork.Repository<Workout>().Update(workout);
            await unitOfWork.Repository<Workout>().SaveChanges();
        }
    }
}
