using GymHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.Extensions
{
    public class PasswordValidationExtensionsTest
    {
        private const string upperDigitString = "TEST1";
        private const string lowerLetterString = "test";

        [Fact]
        public void ContainsUpper_ReturnTrue()
        {
            //Act
            var action = PasswordValidationExtensions.ContainsUpper(upperDigitString);

            //Assert
            Assert.True(action);
        }

        [Fact]
        public void ContainsUpper_ReturnFalse()
        {
            //Act
            var action = PasswordValidationExtensions.ContainsUpper(lowerLetterString);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void ContainsDigit_ReturnTrue()
        {
            //Act
            var action = PasswordValidationExtensions.ContainsDigit(upperDigitString);

            //Assert
            Assert.True(action);
        }

        [Fact]
        public void ContainsDigit_ReturnFalse()
        {
            //Act
            var action = PasswordValidationExtensions.ContainsDigit(lowerLetterString);

            //Assert
            Assert.False(action);
        }
    }
}
