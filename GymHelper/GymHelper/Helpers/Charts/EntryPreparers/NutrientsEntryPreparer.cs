using GymHelper.Models;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Helpers.Charts.EntryPreparers
{
    public class NutrientsEntryPreparer : ChartEntryPreparer
    {
        private readonly List<ChartEntry> nutrients;
        private readonly int dietId;

        public NutrientsEntryPreparer(int dietId)
        {
            nutrients = new List<ChartEntry>();
            this.dietId = dietId;
        }

        public override async Task<List<ChartEntry>> PrepareChartEntry()
        {
            var diet = await unitOfWork.Repository<Diet>().ReadFirstByCondition(x => x.DietId == dietId);

            FillChartEntryData(nutrients, diet.TotalCarbohydrates, "Węglowodany");
            FillChartEntryData(nutrients, diet.TotalFats, "Tłuszcze", "#0000FF");
            FillChartEntryData(nutrients, diet.TotalProteins, "Białko", "#00FF00");

            return nutrients;
        }
    }
}