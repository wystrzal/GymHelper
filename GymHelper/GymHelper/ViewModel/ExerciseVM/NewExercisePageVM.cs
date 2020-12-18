using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.BaseVM;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class NewExercisePageVM : NewDataViewModel
    {
        public BaseCommand NewExerciseCommand { get; private set; }
        public NewExercisePageVM()
        {
            NewExerciseCommand = new NewExerciseCommand(this);
            exercise = new Exercise { UserId = App.Data.User.UserId };
        }

        private Exercise exercise;
        public Exercise Exercise
        {
            get { return exercise; }
            set
            {
                exercise = value;
                OnPropertyChanged("Exercise");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                exercise.Name = name.ToLower();
                NewExerciseCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Name");
            }
        }

        public override async Task AddData<TEntity>(TEntity entity)
        {
            if (await ExerciseExist(entity as Exercise))
            {
                await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Istnieje już takie ćwiczenie.", "Ok");
                return;
            }

            await base.AddData(entity);
        }

        private async Task<bool> ExerciseExist(Exercise exercise)
        {
            return await unitOfWork.Repository<Exercise>()
                .CheckIfExistByCondition(x => x.Name == exercise.Name && x.UserId == exercise.UserId);
        }
    }
}
