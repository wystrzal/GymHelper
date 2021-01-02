﻿using GymHelper.Models;
using GymHelper.ViewModel;
using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
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
    public partial class ChartsPage : ContentPage
    {
        private readonly ChartsPageVM viewModel;

        public ChartsPage()
        {
            InitializeComponent();
            viewModel = BindingContext as ChartsPageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lastRepetitionsChart.Chart = new LineChart();
            lastWeightsChart.Chart = new LineChart();
            monthHighestWeightsChart.Chart = new LineChart();
            await viewModel.ReadExercises();
        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;

            if (picker.SelectedItem is Exercise exercise)
            {
                await viewModel.PrepareChartsEntries(exercise);
                CreateNewCharts();
            }
        }

        private void CreateNewCharts()
        {
            lastWeightsChart.Chart = CreateLineChart(viewModel.LastWeights);
            lastRepetitionsChart.Chart = CreateLineChart(viewModel.LastRepetitions);
            monthHighestWeightsChart.Chart = CreateLineChart(viewModel.MonthHighestWeights);
        }

        private LineChart CreateLineChart(IEnumerable<ChartEntry> chartEntries)
        {
            return new LineChart
            {
                Entries = chartEntries,
                LabelTextSize = 42,
                BackgroundColor = SKColor.Parse("#CCC5B9")
            };
        }
    }
}