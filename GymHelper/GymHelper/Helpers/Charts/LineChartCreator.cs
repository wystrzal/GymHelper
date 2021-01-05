﻿using GymHelper.Data.Interfaces;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Charts
{
    public class LineChartCreator : IChartCreator
    {
        public async Task<Chart> CreateChart(ChartEntryPreparer chartEntryPreparer)
        {
            return new LineChart
            {
                Entries = await chartEntryPreparer.PrepareChartEntry(),
                LabelTextSize = 48,
                BackgroundColor = SKColors.Transparent,
                LabelColor = SKColor.Parse("#EB5E28")
            };
        }
    }
}