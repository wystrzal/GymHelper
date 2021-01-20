using GymHelper.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.Helpers.Converters
{
    public class CleanWhenAppearConverterTest
    {
        private readonly CleanWhenAppearConverter converter;
        private const string testString = "test";

        public CleanWhenAppearConverterTest()
        {
            converter = new CleanWhenAppearConverter();
        }

        [Fact]
        public void Convert_Success()
        {
            //Act
            var testValue = converter.Convert(testString, typeof(string), null, null).ToString();

            //Assert
            Assert.True(string.IsNullOrWhiteSpace(testValue));
        }

        [Fact]
        public void ConvertBack_Success()
        {
            //Act
            var testValue = converter.ConvertBack(testString, typeof(string), null, null).ToString();

            //Assert
            Assert.Equal(testString, testValue);
        }
    }
}