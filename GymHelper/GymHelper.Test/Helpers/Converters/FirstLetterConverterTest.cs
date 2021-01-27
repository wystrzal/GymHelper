using GymHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.Helpers.Converters
{
    public class FirstLetterConverterTest
    {
        private readonly FirstLetterConverter converter;
        private const string testString = "test";

        public FirstLetterConverterTest()
        {
            converter = new FirstLetterConverter();
        }

        [Fact]
        public void Convert_Success()
        {
            //Act
            var testValue = converter.Convert(testString, typeof(string), null, null).ToString();

            //Assert
            Assert.Equal("Test", testValue);
        }

        [Fact]
        public void ConvertBack_NotImplementedException()
        {
            //Assert
            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(testString, typeof(string), null, null));
        }
    }
}
