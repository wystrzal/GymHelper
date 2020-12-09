using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers;
using GymHelper.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymHelper.Test.Service
{
    public class UnitOfWorkTest
    {
        private readonly UnitOfWork unitOfWork;

        public UnitOfWorkTest()
        {
            unitOfWork = new UnitOfWork();
            App.Data = new DataStorage(It.IsAny<string>());
        }

        [Fact]
        public void Repository_Success()
        {
            //Act
            var action = unitOfWork.Repository<User>();

            //Assert
            Assert.Equal(typeof(Repository<User>) ,action.GetType());
        }
    }
}
