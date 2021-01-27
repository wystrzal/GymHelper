using GymHelper.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.Helpers
{
    public class AppServiceProviderTest
    {
        [Fact]
        public void BuildServiceProvider_Success()
        {
            //Act
            var action = AppServiceProvider.BuildServiceProvider();

            //Assert
            Assert.Equal(typeof(ServiceProvider), action.GetType());
            Assert.NotNull(action);
        }
    }
}
