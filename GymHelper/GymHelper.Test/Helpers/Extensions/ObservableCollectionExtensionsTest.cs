using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xunit;
using GymHelper.Data.Services;

namespace GymHelper.Test.Helpers.Extensions
{
    public class ObservableCollectionExtensionsTest
    {
        [Fact]
        public void FillCollection_Success()
        {
            //Arrange
            var collection = new ObservableCollection<Product>();
            var dataList = new List<Product> { new Product(), new Product() };

            //Act
            collection.FillCollection(dataList);

            //Assert
            Assert.Equal(dataList.Count, collection.Count);
        }
    }
}
