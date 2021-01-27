using GymHelper.Data.Interfaces;
using GymHelper.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Charts.ChartPreparers
{
    public class ExerciseChartPreparer : ChartPreparer<Exercise>
    {
        public ExerciseChartPreparer() : base(new LineChartCreator())
        {
        }

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

        public override async Task PrepareCharts(Exercise entity)
        {
            LastWeightsChart = await chartCreator.CreateChart(new LastWeightsEntryPreparer(entity.ExerciseId));
            LastRepetitionsChart = await chartCreator.CreateChart(new LastRepetitionsEntryPreparer(entity.ExerciseId));
            MonthHighestWeightsChart = await chartCreator.CreateChart(new MonthHighestWeightsEntryPreparer(entity.ExerciseId));
        }
    }
}
