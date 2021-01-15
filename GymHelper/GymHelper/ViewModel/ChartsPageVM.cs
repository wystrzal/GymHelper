using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers.Charts;
using GymHelper.Helpers.Charts.ChartPreparers;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.BaseVM;
using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.ViewModel
{
    public class ChartsPageVM : ReadDataViewModel<Exercise>
    {
        public ChartPreparer<Exercise> ChartPreparer { get; private set; }

        public ChartsPageVM()
        {
            ChartPreparer = new ExerciseChartPreparer();
        }

        public override async Task<IEnumerable<Exercise>> GetData(int pageIndex, int pageSize = 10)
        {
            return await unitOfWork.Repository<Exercise>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId, pageSize, pageIndex * pageSize);
        }

        public override async Task SearchData(string query)
        {
            var exercises = await unitOfWork.Repository<Exercise>()
                .ReadAllByCondition(x => x.UserId == App.Data.User.UserId && x.Name.Contains(query));

            Collection.FillCollection(exercises);
        }
    }
}