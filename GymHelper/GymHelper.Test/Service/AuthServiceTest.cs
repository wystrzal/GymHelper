using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using GymHelper.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.Service
{
    public class AuthServiceTest
    {
        private const string username = "test";
        private const string password = "Test123";

        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<IAlertService> alertService;
        private readonly AuthService authService;
        private readonly User user;

        public AuthServiceTest()
        {
            App.Data = new DataStorage(It.IsAny<string>());
            unitOfWork = new Mock<IUnitOfWork>();
            alertService = new Mock<IAlertService>();
            authService = new AuthService(unitOfWork.Object, alertService.Object);
            user = new User { Login = username, Password = password };
        }

        [Fact]
        public async Task LoginTo_NullUser_ReturnFalse()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<User>().ReadFirstByCondition(It.IsAny<Func<User, bool>>()))
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

            unitOfWork.Setup(x => x.Repository<User>().ReadFirstByCondition(It.IsAny<Func<User, bool>>()))
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
            unitOfWork.Setup(x => x.Repository<User>().ReadFirstByCondition(It.IsAny<Func<User, bool>>()))
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
            unitOfWork.Setup(x => x.Repository<User>().CheckIfExistByCondition(It.IsAny<Func<User, bool>>()))
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
            unitOfWork.Setup(x => x.Repository<User>().CheckIfExistByCondition(It.IsAny<Func<User, bool>>()))
                .Returns(Task.FromResult(false));

            unitOfWork.Setup(x => x.SaveChanges()).Returns(Task.FromResult(false));

            //Act
            var action = await authService.Register(user);

            //Assert
            unitOfWork.Verify(x => x.Repository<User>().Add(user), Times.Once);
            Assert.False(action);
        }

        [Fact]
        public async Task Register_SaveChangesSuccess_ReturnTrue()
        {
            //Arrange
            unitOfWork.Setup(x => x.Repository<User>().CheckIfExistByCondition(It.IsAny<Func<User, bool>>()))
                .Returns(Task.FromResult(false));

            unitOfWork.Setup(x => x.SaveChanges()).Returns(Task.FromResult(true));

            //Act
            var action = await authService.Register(user);

            //Assert
            unitOfWork.Verify(x => x.Repository<User>().Add(user), Times.Once);
            Assert.True(action);
        }
    }
}
