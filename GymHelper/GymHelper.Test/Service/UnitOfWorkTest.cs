using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
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
