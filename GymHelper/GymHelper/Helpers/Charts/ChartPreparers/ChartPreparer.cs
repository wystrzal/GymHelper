using GymHelper.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Charts.ChartPreparers
{
    public abstract class ChartPreparer<TEntity> : INotifyPropertyChanged
        where TEntity : class
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected readonly IChartCreator chartCreator;

        public ChartPreparer(IChartCreator chartCreator)
        {
            this.chartCreator = chartCreator;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract Task PrepareCharts(TEntity entity);
    }
}
