using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Chart;
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
    public class ChartsPageVM : ReadDataViewModel<Exercise>
    {
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

        public List<ChartEntry> LastWeights { get; set; }
        public List<ChartEntry> LastRepetitions { get; set; }
        public List<ChartEntry> MonthHighestWeights { get; set; }

        private ChartEntryPreparer chartEntryPreparer;

        public override async Task ReadData()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);
            Collection.FillCollection(exercises);
        }

        public async Task GenerateChartEntries(Exercise exercise)
        {
            if (LastRepetitions != null && LastWeights != null && MonthHighestWeights != null)
            {
                ClearCharts();
            }

            await PrepareChartEntries(exercise.ExerciseId);
        }

        private void ClearCharts()
        {
            LastWeights.Clear();
            LastRepetitions.Clear();
            MonthHighestWeights.Clear();
        }

        private async Task PrepareChartEntries(int exerciseId)
        {
            chartEntryPreparer = new LastWeightsEntryPreparer(exerciseId);
            LastWeights = await chartEntryPreparer.PrepareChartEntry();

            chartEntryPreparer = new LastRepetitionsEntryPreparer(exerciseId);
            LastRepetitions = await chartEntryPreparer.PrepareChartEntry();

            chartEntryPreparer = new MonthHighestWeightsEntryPreparer(exerciseId);
            MonthHighestWeights = await chartEntryPreparer.PrepareChartEntry();

            CreateNewCharts();
        }

        private void CreateNewCharts()
        {
            LastWeightsChart = CreateLineChart(LastWeights);
            LastRepetitionsChart = CreateLineChart(LastRepetitions);
            MonthHighestWeightsChart = CreateLineChart(MonthHighestWeights);
        }

        private LineChart CreateLineChart(IEnumerable<ChartEntry> chartEntries)
        {
            return new LineChart
            {
                Entries = chartEntries,
                LabelTextSize = 48,
                BackgroundColor = SKColors.Transparent,
                LabelColor = SKColor.Parse("#EB5E28")
            };
        }
    }
}
