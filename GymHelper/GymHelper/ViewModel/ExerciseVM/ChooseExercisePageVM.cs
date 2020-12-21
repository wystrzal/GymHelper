using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
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
            => new Command(async () => await NavigateService.Navigate<NewExercisePage>());
        public override ICommand NavigateToEditDataCommand 
            => new Command<Exercise>(async (exercise) => await NavigateService.Navigate<EditExercisePage>(exercise));

        protected override async Task AddSelectedData()
        {
            List<Task> tasks = new List<Task>();

            foreach (var item in SelectedData)
            {
                tasks.Add(AddWorkoutExercise(item));
            }

            await Task.WhenAll(tasks);

            await NavigateService.NavigateBack();
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
                await unitOfWork.Repository<WorkoutExercise>().SaveChanges();
            }
        }

        private async Task<bool> WorkoutExerciseExist(WorkoutExercise workoutExercise)
        {
            return await unitOfWork.Repository<WorkoutExercise>()
                .CheckIfExistByCondition(x => x.ExerciseId == workoutExercise.ExerciseId && x.WorkoutId == workoutExercise.WorkoutId);
        }

        public override async Task ReadData()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);

            Collection.FillCollection(exercises);
        }
    }
}
