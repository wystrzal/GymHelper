using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Helpers;
using GymHelper.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.Service
{
    public class AuthServiceTest
    {
        private const string username = "test";
        private const string password = "Test123";
        private const string repeatPassword = "Test123";

        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<IAlertService> alertService;
        private readonly AuthService authService;
        private readonly User user;

        public AuthServiceTest()
        {
            TestHelper.PrepareUnitOfWork(out unitOfWork, out alertService);
            authService = new AuthService(unitOfWork.Object, alertService.Object);
            user = new User { Login = username, Password = password, RepeatPassword = repeatPassword, UserId = 1 };
        }

        [Fact]
        public async Task LoginTo_NullUser_ReturnFalse()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<User>().ReadFirstByCondition(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult((User)null));

            //Act
            var action = await authService.LoginTo(username, password);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task LoginTo_BadPassword_ReturnFalse()
        {
            //Arrange
            user.Password = "badPassword";

            unitOfWork.Setup(x => x.Repository<User>().ReadFirstByCondition(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(user));

            //Act
            var action = await authService.LoginTo(username, password);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task LoginTo_Success_ReturnTrue()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<User>()
                .ReadFirstByConditionWithInclude(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, Diet>>>()))
                .Returns(Task.FromResult(user));

            //Act
            var action = await authService.LoginTo(username, password);

            //Assert
            Assert.True(action);
        }

        [Fact]
        public async Task Register_ShortPassword_ReturnFalse()
        {
            //Arrange
            user.Password = "short";

            //Act
            var action = await authService.Register(user);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task Register_PasswordWithoutDigit_ReturnFalse()
        {
            //Arrange
            user.Password = "password";

            //Act
            var action = await authService.Register(user);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task Register_PasswordWithoutUppercase_ReturnFalse()
        {
            //Arrange
            user.Password = "password1";

            //Act
            var action = await authService.Register(user);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task Register_UserExist_ReturnFalse()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<User>().CheckIfExistByCondition(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(true));

            //Act
            var action = await authService.Register(user);

            //Assert
            alertService.Verify(x => x.DisplayAlert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task Register_SaveChangesFailed_ReturnFalse()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<User>().CheckIfExistByCondition(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(false));

            unitOfWork.Setup(x => x.SaveChanges()).Returns(Task.FromResult(false));
            unitOfWork.Setup(x => x.Repository<User>().Add(user));
            unitOfWork.Setup(x => x.Repository<Diet>().Add(It.IsAny<Diet>()));

            //Act
            var action = await authService.Register(user);

            //Assert
            unitOfWork.Verify(x => x.Repository<User>().Add(user), Times.Once);
            unitOfWork.Verify(x => x.Repository<Diet>().Add(It.IsAny<Diet>()), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task Register_SaveChangesSuccess_ReturnTrue()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<User>().CheckIfExistByCondition(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(false));

            unitOfWork.Setup(x => x.SaveChanges()).Returns(Task.FromResult(true));
            unitOfWork.Setup(x => x.Repository<User>().Add(user));
            unitOfWork.Setup(x => x.Repository<Diet>().Add(It.IsAny<Diet>()));

            //Act
            var action = await authService.Register(user);

            //Assert
            unitOfWork.Verify(x => x.Repository<User>().Add(user), Times.Once);
            unitOfWork.Verify(x => x.Repository<Diet>().Add(It.IsAny<Diet>()), Times.Once);
            Assert.True(action);
        }
    }
}