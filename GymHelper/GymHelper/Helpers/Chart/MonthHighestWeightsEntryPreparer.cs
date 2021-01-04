using GymHelper.Helpers.Extensions;
using GymHelper.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Chart
{
    public class MonthHighestWeightsEntryPreparer : ChartEntryPreparer
    {
        private readonly List<ChartEntry> MonthHighestWeights;
        private readonly List<int> numberOfMonths;
        private readonly int exerciseId;

        public MonthHighestWeightsEntryPreparer(int exerciseId)
        {
            MonthHighestWeights = new List<ChartEntry>();
            numberOfMonths = Enumerable.Range(1, 12).ToList();
            this.exerciseId = exerciseId;
        }

        public override async Task<List<ChartEntry>> PrepareChartEntry()
        {
            await numberOfMonths.LoopAsync(FillMonthHighestExercisesWeightEntry);
            return MonthHighestWeights;
        }

        private async Task FillMonthHighestExercisesWeightEntry(int i)
        {
            var workoutExercise = await unitOfWork.Repository<WorkoutExercise>()
                .ReadFirstByCondition(x => x.ExerciseId == exerciseId && x.Date.Month == i, x => x.Weight, false);

            if (workoutExercise != null)
            {
                FillChartEntryData(MonthHighestWeights, workoutExercise.Weight,
                    workoutExercise.Date.ToString("MMMM dd", new CultureInfo("pl-PL")).Capitalize());
            }
        }
    }
}
