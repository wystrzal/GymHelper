using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    class NewExercisePageVM : BaseViewModel
    {
        public NewExerciseCommand NewExerciseCommand { get; private set; }
        private readonly IUnitOfWork unitOfWork;
        public NewExercisePageVM()
        {
            unitOfWork = App.Data.UnitOfWork;
            NewExerciseCommand = new NewExerciseCommand(this);
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
                Exercise = new Exercise()
                {
                    Name = Name
                };
                OnPropertyChanged("Name");
            }
        }

        public async Task AddExercise(Exercise exercise)
        {
            if (await ExerciseExist(exercise))
            {
                await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Istnieje już takie ćwiczenie.", "Ok");
                return;
            }

            exercise.UserId = App.Data.User.UserId;
            await unitOfWork.Repository<Exercise>().Add(exercise);
            var exerciseAdded = await unitOfWork.Repository<Exercise>().SaveChanges();
            
            if (exerciseAdded)
            {
                await NavigateService.NavigateBack();
            }
        }

        private async Task<bool> ExerciseExist(Exercise exercise)
        {
            return await unitOfWork.Repository<Exercise>()
                .CheckIfExistByCondition(x => x.Name == exercise.Name && x.UserId == exercise.UserId);
        }
    }
}
