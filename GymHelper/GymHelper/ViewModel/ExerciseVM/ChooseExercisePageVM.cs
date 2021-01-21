using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Extensions;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.BaseVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class ChooseExercisePageVM : ChooseDataViewModel<Exercise>
    {
        public override ICommand NavigateToAddDataCommand 
            => new Command(async () => await navigateService.Navigate<NewExercisePage>());
        public override ICommand NavigateToEditDataCommand 
            => new Command<Exercise>(async (exercise) => await navigateService.Navigate<EditExercisePage>(exercise));

        protected override async Task<int> GetDataCount()
        {
            return await unitOfWork.Repository<Exercise>().ReadDataCount(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query));
        }

        protected override async Task<IEnumerable<Exercise>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<Exercise>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query), pageSize, pageIndex * pageSize);
        }

        protected override async Task AddSelectedData()
        {
            await SelectedData.LoopAsync(AddWorkoutExercise);
            await navigateService.NavigateBack();
        }

        private async Task AddWorkoutExercise(Exercise exercise)
        {
            var workoutExercise = new WorkoutExercise
            {
                ExerciseId = exercise.ExerciseId,
                WorkoutId = App.Data.Workout.WorkoutId
            };

            if (!await WorkoutExerciseExist(workoutExercise))
            {
                await unitOfWork.Repository<WorkoutExercise>().Add(workoutExercise);
                await unitOfWork.SaveChanges();
            }
        }

        private async Task<bool> WorkoutExerciseExist(WorkoutExercise workoutExercise)
        {
            return await unitOfWork.Repository<WorkoutExercise>()
                .CheckIfExistByCondition(x => x.ExerciseId == workoutExercise.ExerciseId && x.WorkoutId == workoutExercise.WorkoutId);
        }
    }
}