using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IChartPreparer<TEntity> where TEntity : class
    {
        Task PrepareCharts(TEntity entity);
    }
}
