using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GymHelper.Helpers.Extensions;

namespace GymHelper.Test.Helpers.Extensions
{
    public class StringExtensionsTest
    {
        [Fact]
        public void Capitalize_EmptyString_Success()
        {
            //Arrange
            var testString = "";

            //Act
            testString = testString.Capitalize();

            //Assert
            Assert.True(string.IsNullOrWhiteSpace(testString));
        }

        [Fact]
        public void Capitalize_Success()
        {
            //Arrange
            var testString = "test";

            //Act
            testString = testString.Capitalize();

            //Assert
            Assert.Equal("Test", testString);
        }
    }
}
