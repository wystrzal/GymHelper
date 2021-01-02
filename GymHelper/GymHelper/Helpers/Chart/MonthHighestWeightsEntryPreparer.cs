using GymHelper.Helpers.Extensions;
using GymHelper.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Chart
{
    public class MonthHighestWeightsEntryPreparer : ChartEntryPreparer
    {
        private readonly List<ChartEntry> MonthHighestWeights;
        private const int monthNumber = 12;

        public MonthHighestWeightsEntryPreparer()
        {
            MonthHighestWeights = new List<ChartEntry>();
        }

        public override async Task<List<ChartEntry>> GetChartEntry(Exercise exercise)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 1; i <= monthNumber; i++)
            {
                tasks.Add(FillMonthHighestExercisesWeightEntry(exercise, i));
            }

            await Task.WhenAll(tasks);

            return MonthHighestWeights;
        }

        private async Task FillMonthHighestExercisesWeightEntry(Exercise exercise, int i)
        {
            var workoutExercise = await unitOfWork.Repository<WorkoutExercise>()
                .ReadFirstByCondition(x => x.ExerciseId == exercise.ExerciseId && x.Date.Month == i, x => x.Weight, false);

            if (workoutExercise != null)
            {
                FillChartEntryData(MonthHighestWeights, workoutExercise.Weight,
                    workoutExercise.Date.ToString("MMMM dd", new CultureInfo("pl-PL")).Capitalize());
            }
        }
    }
}
