using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class EditExercisePageVM : EditDataViewModel<Exercise>
    {
        public override ICommand EditDataCommand
            => new Command<Exercise>(async (exercise) => await Update(exercise));
        public ICommand NameChangedCommand => new Command<string>((text)
            => Exercise.Name = (string)RestoreOldValue(text, OldExercise.Name, Exercise.Name));

        public Exercise OldExercise { get; private set; }

        private Exercise exercise;
        public Exercise Exercise
        {
            get { return exercise; }
            set
            {
                exercise = value;
                if (OldExercise == null)
                {
                    OldExercise = (Exercise)exercise.Clone();
                }
                OnPropertyChanged("Exercise");
            }
        }

        public override async Task Update(Exercise entity)
        {
            entity.Name = entity.Name.ToLower();
            await base.Update(entity);
        }
    }
}