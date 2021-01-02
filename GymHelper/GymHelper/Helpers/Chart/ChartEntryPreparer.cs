using GymHelper.Data.Interfaces;
using GymHelper.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Chart
{
    public abstract class ChartEntryPreparer
    {
        protected readonly IUnitOfWork unitOfWork;

        public ChartEntryPreparer()
        {
            unitOfWork = App.Data.UnitOfWork;
        }

        public abstract Task<List<ChartEntry>> GetChartEntry(Exercise exercise);

        protected void FillChartEntryData(List<ChartEntry> chartEntries, float value, string label)
        {     
            var chartEntry = new ChartEntry(value)
            {
                Color = SKColor.Parse("#FF1493"),
                ValueLabel = value.ToString(),
                Label = label
            };

            chartEntries.Add(chartEntry);
        }
    }
}
