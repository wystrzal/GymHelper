using GymHelper.Data.Interfaces;
using GymHelper.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Charts
{
    public abstract class ChartEntryPreparer
    {
        protected readonly IUnitOfWork unitOfWork;

        public ChartEntryPreparer()
        {
            unitOfWork = App.Data.UnitOfWork;
        }

        public abstract Task<List<ChartEntry>> PrepareChartEntry();

        protected void FillChartEntryData(List<ChartEntry> chartEntries, float value, string label, string color = null)
        {
            var chartEntry = new ChartEntry(value)
            {
                Color = SKColor.Parse(color ?? "#FF0000"),
                ValueLabel = value.ToString(),
                Label = label,
                TextColor = SKColor.Parse("#EB5E28"),
                ValueLabelColor = SKColor.Parse("#EB5E28")
            };

            chartEntries.Add(chartEntry);
        }
    }
}
