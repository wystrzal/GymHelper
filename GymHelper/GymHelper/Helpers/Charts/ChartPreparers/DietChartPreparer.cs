using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Charts.EntryPreparers;
using GymHelper.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Charts.ChartPreparers
{
    public class DietChartPreparer : IChartPreparer<Diet>, INotifyPropertyChanged
    {
        private readonly IChartCreator chartCreator;
        public event PropertyChangedEventHandler PropertyChanged;

        public DietChartPreparer()
        {
            chartCreator = new DonutChartCreator();
        }

        private Chart nutrientsChart;
        public Chart NutrientsChart
        {
            get { return nutrientsChart; }
            set
            {
                nutrientsChart = value;
                OnPropertyChanged("NutrientsChart");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task PrepareCharts(Diet entity)
        {
            NutrientsChart = await chartCreator.CreateChart(new NutrientsEntryPreparer(entity.DietId));
        }
    }
}
