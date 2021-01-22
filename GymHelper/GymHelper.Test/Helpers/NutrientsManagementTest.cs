using GymHelper.Helpers;
using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.Helpers
{
    public class NutrientsManagementTest
    {
        private readonly Product product;
        private readonly Diet diet;

        public NutrientsManagementTest()
        {
            product = new Product { Calories = 100, Carbohydrates = 100, Fats = 100, Proteins = 100 };
            diet = new Diet { TotalCalories = 100, TotalCarbohydrates = 100, TotalFats = 100, TotalProteins = 100 };
        }

        [Fact]
        public void SubtractNutrients_Success()
        {
            //Act
            NutrientsManagement.SubtractNutrients(product, diet);

            //Arrange
            Assert.Equal(0, diet.TotalCalories);
            Assert.Equal(0, diet.TotalCarbohydrates);
            Assert.Equal(0, diet.TotalFats);
            Assert.Equal(0, diet.TotalProteins);
        }

        [Fact]
        public void AddNutrients_Success()
        {
            //Act
            NutrientsManagement.AddNutrients(product, diet);

            //Arrange
            Assert.Equal(200, diet.TotalCalories);
            Assert.Equal(200, diet.TotalCarbohydrates);
            Assert.Equal(200, diet.TotalFats);
            Assert.Equal(200, diet.TotalProteins);
        }
    }
}