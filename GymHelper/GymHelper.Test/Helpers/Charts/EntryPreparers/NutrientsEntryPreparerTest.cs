using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Charts.EntryPreparers;
using GymHelper.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.Helpers.Charts.EntryPreparers
{
    public class NutrientsEntryPreparerTest
    {
        private const int chartEntriesCount = 3;

        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly NutrientsEntryPreparer entryPreparer;
        private readonly Diet diet;

        public NutrientsEntryPreparerTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            entryPreparer = new NutrientsEntryPreparer(It.IsAny<int>());
            diet = new Diet();
        }

        [Fact]
        public async Task PrepareChartEntry_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<Diet>().ReadFirstByCondition(It.IsAny<Expression<Func<Diet, bool>>>())).Returns(Task.FromResult(diet));

            //Act
            var action = await entryPreparer.PrepareChartEntry();

            //Assert
            Assert.NotNull(action);
            Assert.Equal(chartEntriesCount, action.Count);
        }
    }
}