using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.View.ExerciseView;
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
    public class WorkoutPageVM : DisplayDataViewModel<Workout>
    {
        public override ICommand NavigateToAddDataCommand
            => new Command(async () => await NavigateService.Navigate<NewWorkoutPage>());
        public override ICommand NavigateToEditDataCommand
             => new Command<Workout>(async (workout) => await NavigateService.Navigate<EditWorkoutPage>(workout));

        public Workout SelectedWorkout
        {
            get => null;
            set
            {
                if (value != null)
                {
                    App.Data.Workout = value;
                    NavigateService.Navigate<WorkoutExercisePage>();
                }
                OnPropertyChanged("SelectedWorkout");
            }
        }

        protected override async Task<IEnumerable<Workout>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<Workout>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query),
                y => y.Date, orderASC: false, take: pageSize, skip: pageIndex * pageSize);
        }

        protected override async Task<int> GetDataCount()
        {
            return await unitOfWork.Repository<Workout>().ReadDataCount(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query));
        }
    }
}