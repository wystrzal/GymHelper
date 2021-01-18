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
    public class EditWorkoutPageVM : EditDataViewModel<Workout>
    {
        public override ICommand EditDataCommand
            => new Command<Workout>(async (workout) => await Update(workout));
        public ICommand NameChangedCommand => new Command<string>((text)
            => Workout.Name = (string)RestoreOldValue(text, OldWorkout.Name, Workout.Name));

        public Workout OldWorkout { get; private set; }

        private Workout workout;
        public Workout Workout
        {
            get => workout;
            set
            {
                workout = value;
                if (OldWorkout == null)
                {
                    OldWorkout = (Workout)workout.Clone();
                }
                OnPropertyChanged("Workout");
            }
        }

        public override Task Update(Workout entity)
        {
            entity.Name = entity.Name.ToLower();
            return base.Update(entity);
        }
    }
}