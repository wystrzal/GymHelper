using GymHelper.Models;
using GymHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GymHelper.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWorkoutExercisePage : ContentPage
    {
        public EditWorkoutExercisePage(WorkoutExercise workoutExercise)
        {
            InitializeComponent();
            ((EditWorkoutExercisePageVM)BindingContext).WorkoutExercise = workoutExercise;
        }

        private void SeriesEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditWorkoutExercisePageVM)BindingContext).WorkoutExercise.Series
                    = ((EditWorkoutExercisePageVM)BindingContext).OldWorkoutExercise.Series;
            }
        }

        private void RepetitionEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditWorkoutExercisePageVM)BindingContext).WorkoutExercise.Repetition
                    = ((EditWorkoutExercisePageVM)BindingContext).OldWorkoutExercise.Repetition;
            }
        }

        private void WeightEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((Entry)sender).Text))
            {
                ((EditWorkoutExercisePageVM)BindingContext).WorkoutExercise.Weight
                    = ((EditWorkoutExercisePageVM)BindingContext).OldWorkoutExercise.Weight;
            }
        }
    }
}