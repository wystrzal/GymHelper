using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class WorkoutPageVM : BaseViewModel
    {
        public ObservableCollection<Workout> Workouts { get; set; }
        public ICommand NewWorkoutNavCommand => new Command(async () => await NavigateService.Navigate<NewWorkoutPage>());
        public ICommand DeleteWorkoutCommand
            => new Command<Workout>(async (workout) => await DeleteWorkout(workout));
        public ICommand EditWorkoutNavCommand
            => new Command<Workout>(async (workout) => await NavigateService.Navigate<EditWorkoutPage>(workout));

        private readonly IUnitOfWork unitOfWork;
        public WorkoutPageVM()
        {
            Workouts = new ObservableCollection<Workout>();
            unitOfWork = App.Data.UnitOfWork;
        }

        public async Task ReadWorkouts()
        {
            var workouts = await unitOfWork.Repository<Workout>()
                .ReadAllByCondition(workout => workout.UserId == App.Data.User.UserId, x => x.Date, false);

            Workouts.Clear();

            if (workouts != null && workouts.Count > 0)
            {
                foreach (var workout in workouts)
                {
                    Workouts.Add(workout);
                }
            }
        }

        private async Task DeleteWorkout(Workout workout)
        {
            await unitOfWork.Repository<Workout>().Delete(workout);
            await unitOfWork.Repository<Workout>().SaveChanges();
            await ReadWorkouts();
        }
    }
}
