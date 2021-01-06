using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymHelper.Helpers
{
    public static class NutrientsManagement
    {
        public static void SubtractNutrients(Product entity, Diet diet)
        {
            diet.TotalCalories -= entity.Calories;
            diet.TotalCarbohydrates -= entity.Carbohydrates;
            diet.TotalFats -= entity.Fats;
            diet.TotalProteins -= entity.Proteins;
        }

        public static void AddNutrients(Product product, Diet diet)
        {
            diet.TotalCalories += product.Calories;
            diet.TotalCarbohydrates += product.Carbohydrates;
            diet.TotalFats += product.Fats;
            diet.TotalProteins += product.Proteins;
        }
    }
}
