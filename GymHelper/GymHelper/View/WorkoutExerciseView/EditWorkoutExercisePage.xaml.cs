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
    }
}