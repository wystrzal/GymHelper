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
    public class ExerciseChartPreparerTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly ExerciseChartPreparer chartPreparer;

        public ExerciseChartPreparerTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork);
            chartPreparer = new ExerciseChartPreparer();
        }

        [Fact]
        public async Task PrepareCharts()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .ReadAllByCondition(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Func<WorkoutExercise, DateTime>>(), It.IsAny<int>(),
                    It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new List<WorkoutExercise>()));

            unitOfWork.Setup(x => x.Repository<WorkoutExercise>()
                .ReadFirstByCondition(It.IsAny<Func<WorkoutExercise, bool>>(), It.IsAny<Func<WorkoutExercise, int>>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new WorkoutExercise()));

            //Act
            await chartPreparer.PrepareCharts(new Exercise());

            //Assert
            Assert.Equal(typeof(LineChart), chartPreparer.LastWeightsChart.GetType());
            Assert.Equal(typeof(LineChart), chartPreparer.LastRepetitionsChart.GetType());
            Assert.Equal(typeof(LineChart), chartPreparer.MonthHighestWeightsChart.GetType());
            Assert.NotNull(chartPreparer.LastWeightsChart);
            Assert.NotNull(chartPreparer.LastRepetitionsChart);
            Assert.NotNull(chartPreparer.MonthHighestWeightsChart);
        }
    }
}
