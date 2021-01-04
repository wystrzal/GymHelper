﻿using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Chart;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.BaseVM;
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
    public class ChartsPageVM : ReadDataViewModel<Exercise>
    {
        public List<ChartEntry> LastWeights { get; set; }
        public List<ChartEntry> LastRepetitions { get; set; }
        public List<ChartEntry> MonthHighestWeights { get; set; }

        private ChartEntryPreparer chartEntryPreparer;

        public override async Task ReadData()
        {
            var exercises = await unitOfWork.Repository<Exercise>().ReadAllByCondition(x => x.UserId == App.Data.User.UserId);
            Collection.FillCollection(exercises);
        }

        public async Task PrepareChartsEntries(Exercise exercise)
        {
            if (LastRepetitions != null && LastWeights != null && MonthHighestWeights != null)
            {
                ClearCharts();
            }
          
            await GetChartsEntries(exercise.ExerciseId);
        }

        private void ClearCharts()
        {
            LastWeights.Clear();
            LastRepetitions.Clear();
            MonthHighestWeights.Clear();
        }

        private async Task GetChartsEntries(int exerciseId)
        {
            chartEntryPreparer = new LastWeightsEntryPreparer(exerciseId);
            LastWeights = await chartEntryPreparer.GetChartEntry();

            chartEntryPreparer = new LastRepetitionsEntryPreparer(exerciseId);
            LastRepetitions = await chartEntryPreparer.GetChartEntry();

            chartEntryPreparer = new MonthHighestWeightsEntryPreparer(exerciseId);
            MonthHighestWeights = await chartEntryPreparer.GetChartEntry();
        }
    }
}
