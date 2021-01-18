using GymHelper.Helpers.Charts;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IChartCreator
    {
        Task<Chart> CreateChart(ChartEntryPreparer chartEntryPreparer);
    }
}
