using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Chart;
using GymHelper.Models;
using GymHelper.View;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class ChartsPageVM : BaseViewModel
    {
        public ObservableCollection<Exercise> Exercises { get; set; }
        public List<ChartEntry> LastWeights { get; set; }
        public List<ChartEntry> LastRepetitions { get; set; }
        public List<ChartEntry> MonthHighestWeights { get; set; }

        private ChartEntryPreparer chartEntryPreparer;

        public ChartsPageVM()
        {
            Exercises = new ObservableCollection<Exercise>();
        }

        public async Task ReadExercises()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);
            Exercises.FillCollection(exercises);
        }

        public async Task PrepareChartsEntries(Exercise exercise)
        {
            if (LastRepetitions != null && LastWeights != null && MonthHighestWeights != null)
            {
                ClearCharts();
            }
          
            await GetChartsEntries(exercise);
        }

        private void ClearCharts()
        {
            LastWeights.Clear();
            LastRepetitions.Clear();
            MonthHighestWeights.Clear();
        }
        private async Task GetChartsEntries(Exercise exercise)
        {
            chartEntryPreparer = new LastWeightsEntryPreparer();
            LastWeights = await chartEntryPreparer.GetChartEntry(exercise);

            chartEntryPreparer = new LastRepetitionsEntryPreparer();
            LastRepetitions = await chartEntryPreparer.GetChartEntry(exercise);

            chartEntryPreparer = new MonthHighestWeightsEntryPreparer();
            MonthHighestWeights = await chartEntryPreparer.GetChartEntry(exercise);
        }
    }
}
