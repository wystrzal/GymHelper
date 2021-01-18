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
        private readonly IAlertService alertService = App.Data.AlertService;
        public override ICommand NavigateToAddDataCommand
            => new Command(async () => await NavigateService.Navigate<ChooseExercisePage>());
        public override ICommand NavigateToEditDataCommand
            => new Command(async (data) => await NavigateService.Navigate<EditWorkoutExercisePage>(data));
        public ICommand CopyDataCommand => new Command(async () => await CopyData());

        protected override async Task<IEnumerable<WorkoutExercise>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<WorkoutExercise>()
                .ReadAllByConditionWithInclude(x => x.WorkoutId == App.Data.Workout.WorkoutId && x.Exercise.Name.Contains(query),
                y => y.Exercise, pageSize, pageIndex * pageSize);
        }

        protected override async Task<int> GetDataCount()
        {
            return await unitOfWork.Repository<WorkoutExercise>()
                .ReadDataCount(x => x.WorkoutId == App.Data.Workout.WorkoutId && x.Exercise.Name.Contains(query));
        }

        private async Task CopyData()
        {
            var workout = await unitOfWork.Repository<Workout>()
                .ReadFirstByConditionWithInclude(x => x.WorkoutId == App.Data.Workout.WorkoutId, y => y.WorkoutsExercises);

            var workoutCopy = CopyWorkout(workout);

            await unitOfWork.Repository<Workout>().Add(workoutCopy);

            if (await unitOfWork.SaveChanges())
            {
                CopyWorkoutExercises(workout, workoutCopy);

                if (await unitOfWork.SaveChanges())
                {
                    await alertService.DisplayAlert("Powodzenie", "Trening został skopiowany", "Ok");
                }
            }
        }

        private Workout CopyWorkout(Workout workout)
        {
            var workoutCopy = (Workout)workout.Clone();
            workoutCopy.WorkoutId = 0;
            workoutCopy.Date = DateTime.Now;
            workoutCopy.Name += " (Kopia)";
            workoutCopy.WorkoutsExercises = new List<WorkoutExercise>();
            return workoutCopy;
        }

        private void CopyWorkoutExercises(Workout workout, Workout workoutCopy)
        {
            foreach (var workoutExercise in workout.WorkoutsExercises)
            {
                var workoutExerciseCopy = new WorkoutExercise
                {
                    Repetition = workoutExercise.Repetition,
                    Series = workoutExercise.Series,
                    Weight = workoutExercise.Weight,
                    WorkoutId = workoutExercise.WorkoutId,
                    ExerciseId = workoutExercise.ExerciseId
                };
                workoutCopy.WorkoutsExercises.Add(workoutExerciseCopy);
            }
        }
    }
}