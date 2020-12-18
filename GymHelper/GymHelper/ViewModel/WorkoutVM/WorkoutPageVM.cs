using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.BaseVM;
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
    public class WorkoutPageVM : DisplayDataViewModel
    {
        public ObservableCollection<Workout> Workouts { get; set; }
        public ICommand NewWorkoutNavCommand => new Command(async () => await NavigateService.Navigate<NewWorkoutPage>());
        public ICommand DeleteWorkoutCommand
            => new Command<Workout>(async (workout) => await DeleteData(workout));
        public ICommand EditWorkoutNavCommand
            => new Command<Workout>(async (workout) => await NavigateService.Navigate<EditWorkoutPage>(workout));

        public WorkoutPageVM()
        {
            Workouts = new ObservableCollection<Workout>();
        }

        public override async Task ReadData()
        {
            var workouts = await unitOfWork.Repository<Workout>()
                .ReadAllByCondition(workout => workout.UserId == App.Data.User.UserId, x => x.Date, false);

            Workouts.FillCollection(workouts);
        }
    }
}
