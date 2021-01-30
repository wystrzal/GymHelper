using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Charts;
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
    public class MonthHighestWeightsEntryPreparerTest
    {
        private const int numberOfMonths = 12;

        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly MonthHighestWeightsEntryPreparer entryPreparer;
        private readonly WorkoutExercise workoutExercise;

        public MonthHighestWeightsEntryPreparerTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            entryPreparer = new MonthHighestWeightsEntryPreparer(It.IsAny<int>());
            workoutExercise = new WorkoutExercise();
        }

        [Fact]
        public async Task PrepareChartEntry_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
            .ReadFirstByCondition(It.IsAny<Expression<Func<WorkoutExercise, bool>>>(),
                It.IsAny<Expression<Func<WorkoutExercise, int>>>(), It.IsAny<bool>()))
            .Returns(Task.FromResult(workoutExercise));

            //Act
            var action = await entryPreparer.PrepareChartEntry();

            //Assert
            Assert.NotNull(action);
            unitOfWork.Verify(x => x.Repository<WorkoutExercise>()
            .ReadFirstByCondition(It.IsAny<Expression<Func<WorkoutExercise, bool>>>(),
                It.IsAny<Expression<Func<WorkoutExercise, int>>>(), It.IsAny<bool>()),
                Times.Exactly(numberOfMonths));
        }
    }
}