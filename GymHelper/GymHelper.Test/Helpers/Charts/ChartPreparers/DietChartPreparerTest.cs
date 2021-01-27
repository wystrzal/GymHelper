using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Charts.ChartPreparers;
using GymHelper.Models;
using Microcharts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.Helpers.Charts.ChartPreparers
{
    public class DietChartPreparerTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly DietChartPreparer chartPreparer;

        public DietChartPreparerTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            chartPreparer = new DietChartPreparer();
        }

        [Fact]
        public async Task PrepareCharts_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Diet>().ReadFirstByCondition(It.IsAny<Func<Diet, bool>>()))
                .Returns(Task.FromResult(new Diet()));

            //Act
            await chartPreparer.PrepareCharts(new Diet());

            //Assert
            Assert.Equal(typeof(DonutChart), chartPreparer.NutrientsChart.GetType());
            Assert.NotNull(chartPreparer.NutrientsChart);
        }
    }
}