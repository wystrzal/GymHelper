using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Charts;
using GymHelper.Helpers.Charts.ChartPreparers;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.BaseVM;
using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class ChartsPageVM : ReadDataViewModel<Exercise>
    {
        public ChartPreparer<Exercise> ChartPreparer { get; private set; }

        public ICommand SwitchViewCommand => new Command(() => SwitchCurrentView());

        public ChartsPageVM()
        {
            ChartPreparer = new ExerciseChartPreparer();
        }

        private bool exerciseSelectorIsVisible = true;
        public bool ExerciseSelectorIsVisible
        {
            get
            {
                return exerciseSelectorIsVisible;
            }
            set
            {
                exerciseSelectorIsVisible = value;
                OnPropertyChanged("ExerciseSelectorIsVisible");
            }
        }

        private bool chartsIsVisible = false;
        public bool ChartsIsVisible
        {
            get
            {
                return chartsIsVisible;
            }
            set
            {
                chartsIsVisible = value;
                OnPropertyChanged("ChartsIsVisible");
            }
        }

        public Exercise SelectedExercise
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    ChartPreparer.PrepareCharts(value);
                    SwitchCurrentView();
                }
                OnPropertyChanged("SelectedExercise");
            }
        }

        private void SwitchCurrentView()
        {
            ChartsIsVisible = !ChartsIsVisible;
            ExerciseSelectorIsVisible = !ExerciseSelectorIsVisible;
        }

        protected override async Task<IEnumerable<Exercise>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<Exercise>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query),
                pageSize, pageIndex * pageSize);
        }

        protected override async Task<int> GetDataCount()
        {
            return await unitOfWork.Repository<Exercise>().ReadDataCount(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query));
        }
    }
}