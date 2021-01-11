using GymHelper.Data.Interfaces;
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
    public partial class EditWorkoutPage : ContentPage
    {
        public EditWorkoutPage(Workout workout)
        {
            InitializeComponent();
            ((EditWorkoutPageVM)BindingContext).Workout = workout;
        }

        private void NameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((EditWorkoutPageVM)BindingContext).Workout.Name = ((EditWorkoutPageVM)BindingContext).OldWorkout.Name;
        }
    }
}