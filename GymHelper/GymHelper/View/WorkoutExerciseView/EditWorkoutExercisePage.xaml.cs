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
            var viewModel = BindingContext as EditWorkoutExercisePageVM;
            viewModel.WorkoutExercise = workoutExercise;
            seriesEntry.Text = workoutExercise.Series == 0 ? string.Empty : workoutExercise.Series.ToString();
            repetitionEntry.Text = workoutExercise.Repetition == 0 ? string.Empty : workoutExercise.Repetition.ToString();
            weightEntry.Text = workoutExercise.Weight == 0 ? string.Empty : workoutExercise.Weight.ToString();
        }
    }
}