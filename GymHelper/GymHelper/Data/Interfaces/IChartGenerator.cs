using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IChartGenerator<TEntity> where TEntity : class
    {
        Task GenerateCharts(TEntity entity);
    }
}
