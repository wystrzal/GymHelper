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
    public class WorkoutPageVM : DisplayDataViewModel<Workout>
    {
        public override ICommand NavigateToAddDataCommand 
            => new Command(async () => await NavigateService.Navigate<NewWorkoutPage>());
        public override ICommand NavigateToEditDataCommand
             => new Command<Workout>(async (workout) => await NavigateService.Navigate<EditWorkoutPage>(workout));

        public override async Task<IEnumerable<Workout>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<Workout>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId, y => y.Date, take: pageSize, skip: pageIndex * pageSize);
        }

        public override async Task SearchData(string query)
        {
            var workouts = await unitOfWork.Repository<Workout>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query), y => y.Date, false);

            Collection.FillCollection(workouts);
        }
    }
}
