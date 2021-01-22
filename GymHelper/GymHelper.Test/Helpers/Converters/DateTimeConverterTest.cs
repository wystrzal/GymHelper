using GymHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.Helpers.Converters
{
    public class DateTimeConverterTest
    {
        private readonly DateTimeConverter converter;
        private readonly DateTime testDate;

        public DateTimeConverterTest()
        {
            converter = new DateTimeConverter();
            testDate = new DateTime(2020, 1, 1);
        }

        [Fact]
        public void Convert_Success()
        {
            //Act
            var testValue = converter.Convert(testDate, typeof(string), null, null).ToString();

            //Assert
            Assert.Equal("Stycznia 01", testValue);
        }

        [Fact]
        public void ConvertBack_NotImplementedException()
        {
            //Assert
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(testDate, typeof(string), null, null));
        }
    }
}