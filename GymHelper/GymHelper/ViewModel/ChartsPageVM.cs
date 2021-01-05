using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Charts;
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

namespace GymHelper.ViewModel
{
    public class ChartsPageVM : ReadDataViewModel<Exercise>, IChartGenerator<Exercise>
    {
        private readonly IChartCreator chartCreator;

        private Chart lastWeightsChart;
        public Chart LastWeightsChart
        {
            get { return lastWeightsChart; }
            set
            {
                lastWeightsChart = value;
                OnPropertyChanged("LastWeightsChart");
            }
        }

        private Chart lastRepetitionsChart;
        public Chart LastRepetitionsChart
        {
            get { return lastRepetitionsChart; }
            set
            {
                lastRepetitionsChart = value;
                OnPropertyChanged("LastRepetitionsChart");
            }
        }

        private Chart monthHighestWeightsChart;
        public Chart MonthHighestWeightsChart
        {
            get { return monthHighestWeightsChart; }
            set
            {
                monthHighestWeightsChart = value;
                OnPropertyChanged("MonthHighestWeightsChart");
            }
        }

        public ChartsPageVM()
        {
            chartCreator = new LineChartCreator();
        }

        public override async Task ReadData()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);
            Collection.FillCollection(exercises);
        }

        public async Task GenerateCharts(Exercise exercise)
        {
            LastWeightsChart = await chartCreator.CreateChart(new LastWeightsEntryPreparer(exercise.ExerciseId));
            LastRepetitionsChart = await chartCreator.CreateChart(new LastRepetitionsEntryPreparer(exercise.ExerciseId));
            MonthHighestWeightsChart = await chartCreator.CreateChart(new MonthHighestWeightsEntryPreparer(exercise.ExerciseId));
        }
    }
}