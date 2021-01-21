using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers;
using GymHelper.Helpers.Charts;
using GymHelper.Helpers.Charts.EntryPreparers;
using GymHelper.Models;
using Microcharts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.Helpers.Charts.ChartCreators
{
    public class DonutChartCreatorTest
    {
        private readonly DonutChartCreator chartCreator;
        private readonly NutrientsEntryPreparer entryPreparer;
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Diet diet;

        public DonutChartCreatorTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            chartCreator = new DonutChartCreator();
            entryPreparer = new NutrientsEntryPreparer(It.IsAny<int>());
            diet = new Diet();
        }

        [Fact]
        public async Task CreateChart_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Diet>().ReadFirstByCondition(It.IsAny<Func<Diet, bool>>()))
                .Returns(Task.FromResult(diet));

            //Act
            var chart = await chartCreator.CreateChart(entryPreparer);

            //Assert
            Assert.Equal(typeof(DonutChart), chart.GetType());
            Assert.NotNull(chart);
        }
    }
}