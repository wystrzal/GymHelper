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
    public class ChooseExercisePageVM : DisplayDataViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }
        public ICommand DeleteExerciseCommand 
            => new Command<Exercise>(async (exercise) => await DeleteData(exercise));
        public ICommand NewExerciseNavCommand => new Command(async () => await NavigateService.Navigate<NewExercisePage>());
        public ICommand EditExerciseNavCommand
            => new Command<Exercise>(async (exercise) => await NavigateService.Navigate<EditExercisePage>(exercise));

        public ChooseExercisePageVM()
        {
            Exercises = new ObservableCollection<Exercise>();
        }

        public override async Task ReadData()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);

            Exercises.FillCollection(exercises);
        }
    }
}
