using GymHelper.Data.Interfaces;
using GymHelper.Helpers.Charts;
using GymHelper.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.Helpers.Charts.EntryPreparers
{
    public class LastWeightsEntryPreparerTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly LastWeightsEntryPreparer entryPreparer;
        private readonly List<WorkoutExercise> workoutExercises;

        public LastWeightsEntryPreparerTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            entryPreparer = new LastWeightsEntryPreparer(It.IsAny<int>());
            workoutExercises = new List<WorkoutExercise> { new WorkoutExercise(), new WorkoutExercise() };
        }

        [Fact]
        public async Task PrepareChartEntry_Success()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
            .ReadAllByCondition(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Func<WorkoutExercise, DateTime>>(),
                It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(workoutExercises));

            //Act
            var action = await entryPreparer.PrepareChartEntry();

            //Assert
            Assert.Equal(workoutExercises.Count, action.Count);
            Assert.NotNull(action);
        }
    }
}