using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class NewWorkoutPageVM : BaseViewModel
    {
        public NewWorkoutCommand NewWorkoutCommand { get; private set; }
        private readonly IUnitOfWork unitOfWork;

        public NewWorkoutPageVM()
        {
            NewWorkoutCommand = new NewWorkoutCommand(this);
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
                name = value.ToLower();
                Workout = new Workout()
                {
                    Name = Name,
                    UserId = App.Data.User.UserId
                };
                OnPropertyChanged("Name");
            }
        }

        public async Task AddWorkout(Workout workout)
        {
            await unitOfWork.Repository<Workout>().Add(workout);

            if (await unitOfWork.Repository<Workout>().SaveChanges())
            {
                await NavigateService.NavigateBack();
            }
        }
    }
}
