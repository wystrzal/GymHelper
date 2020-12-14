using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using GymHelper.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    class ChooseExercisePageVM : BaseViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }
        public ICommand DeleteExerciseCommand 
            => new Command<Exercise>(async (exercise) => await DeleteExercise(exercise));

        private readonly IUnitOfWork unitOfWork;

        public ChooseExercisePageVM()
        {
            Exercises = new ObservableCollection<Exercise>();
            unitOfWork = App.Data.UnitOfWork;
        }

        public async Task ReadExercises()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);

            Exercises.Clear();

            if (exercises != null && exercises.Count > 0)
            {
                foreach (var workout in exercises)
                {
                    Exercises.Add(workout);
                }
            }
        }

        private async Task DeleteExercise(Exercise exercise)
        {
            await unitOfWork.Repository<Exercise>().Delete(exercise);
            await ReadExercises();
        }
    }
}
