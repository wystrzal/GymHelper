using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using GymHelper.ViewModel.Commands.ExerciseCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class NewExercisePageVM : BaseViewModel
    {
        public BaseCommand NewExerciseCommand { get; private set; }
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
                name = value.ToLower();
                Exercise = new Exercise()
                {
                    Name = Name
                };
                OnPropertyChanged("Name");
            }
        }

        public async Task AddExercise(Exercise exercise)
        {
            exercise.UserId = App.Data.User.UserId;

            if (await ExerciseExist(exercise))
            {
                await App.Current.MainPage.DisplayAlert("Niepowodzenie", "Istnieje już takie ćwiczenie.", "Ok");
                return;
            }

            await unitOfWork.Repository<Exercise>().Add(exercise); 
            
            if (await unitOfWork.Repository<Exercise>().SaveChanges())
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
