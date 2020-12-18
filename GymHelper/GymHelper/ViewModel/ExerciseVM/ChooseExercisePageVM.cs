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
    public class ChooseExercisePageVM : DisplayDataViewModel<Exercise>
    {
        public override ICommand NavigateToAddDataCommand 
            => new Command(async () => await NavigateService.Navigate<NewExercisePage>());
        public override ICommand NavigateToEditDataCommand 
            => new Command<Exercise>(async (exercise) => await NavigateService.Navigate<EditExercisePage>(exercise));

        public override async Task ReadData()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);

            Collection.FillCollection(exercises);
        }
    }
}
