using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Charts;
using GymHelper.Helpers.Charts.EntryPreparers;
using GymHelper.Models;
using Microcharts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.Helpers.Charts.ChartCreators
{
    public class LineChartCreatorTest
    {
        private readonly LineChartCreator chartCreator;
        private readonly NutrientsEntryPreparer entryPreparer;
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Diet diet;

        public LineChartCreatorTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            chartCreator = new LineChartCreator();
            entryPreparer = new NutrientsEntryPreparer(It.IsAny<int>());
            diet = new Diet();
        }

        [Fact]
        public async Task CreateChart_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Diet>().ReadFirstByCondition(It.IsAny<Expression<Func<Diet, bool>>>()))
                .Returns(Task.FromResult(diet));

            //Act
            var chart = await chartCreator.CreateChart(entryPreparer);

            //Assert
            Assert.Equal(typeof(LineChart), chart.GetType());
            Assert.NotNull(chart);
        }
    }
}
