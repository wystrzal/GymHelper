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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class WorkoutExercisePageVM : DisplayDataViewModel<WorkoutExercise>
    {
        public override ICommand NavigateToAddDataCommand
            => new Command(async () => await NavigateService.Navigate<ChooseExercisePage>());
        public override ICommand NavigateToEditDataCommand
            => new Command(async (data) => await NavigateService.Navigate<EditWorkoutExercisePage>(data));

        protected override async Task<IEnumerable<WorkoutExercise>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<WorkoutExercise>()
                .ReadAllByConditionWithInclude(x => x.WorkoutId == App.Data.Workout.WorkoutId && x.Exercise.Name.Contains(query),
                y => y.Exercise, pageSize, pageIndex * pageSize);
        }

        protected override async Task<int> GetDataCount()
        {
            return await unitOfWork.Repository<WorkoutExercise>().ReadDataCount(x => x.Exercise.Name.Contains(query));
        }
    }
}
