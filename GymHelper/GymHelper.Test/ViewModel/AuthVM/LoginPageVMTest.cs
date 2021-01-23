using GymHelper.Data.Interfaces;
using GymHelper.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using GymHelper.ViewModel;
using System.Threading.Tasks;
using Xunit;
using GymHelper.View;
using GymHelper.Helpers;

namespace GymHelper.Test.ViewModel
{
    public class LoginPageVMTest
    {
        private readonly Mock<IAuthService> authService;
        private readonly Mock<INavigateService> navigateService;
        private readonly User user;
        private readonly LoginPageVM vm;

        public LoginPageVMTest()
        {
            authService = new Mock<IAuthService>();
            navigateService = new Mock<INavigateService>();
            App.Data = new DataStorage(It.IsAny<string>())
            {
                AuthService = authService.Object,
                NavigateService = navigateService.Object
            };
            user = new User { Login = "test", Password = "Test123" };
            vm = new LoginPageVM();
        }

        [Fact]
        public async Task LoginTo_Success()
        {
            //Arrange
            authService.Setup(x => x.LoginTo(user.Login, user.Password)).Returns(Task.FromResult(true));

            //Act
            await vm.LoginTo(user);

            //Assert
            navigateService.Verify(x => x.Navigate<HomePage>(), Times.Once);
        }

        [Fact]
        public async Task LoginTo_Failed()
        {
            //Arrange
            authService.Setup(x => x.LoginTo(user.Login, user.Password)).Returns(Task.FromResult(false));

            //Act
            await vm.LoginTo(user);

            //Assert
            navigateService.Verify(x => x.Navigate<HomePage>(), Times.Never);
        }

        [Fact]
        public void RegisterNavCommand_Success()
        {
            //Act
            vm.RegisterNavCommand.Execute(null);

            //Assert
            navigateService.Verify(x => x.Navigate<RegisterPage>(), Times.Once);
        }

        [Fact]
        public void LoginCommand_CanExecute_NullUser_Failed()
        {
            //Act
            var action = vm.LoginCommand.CanExecute(null);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void LoginCommand_CanExecute_EmptyString_Failed()
        {
            //Arrange
            user.Login = "";

            //Act
            var action = vm.LoginCommand.CanExecute(user);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void LoginCommand_CanExecute_Success()
        {
            //Act
            var action = vm.LoginCommand.CanExecute(user);

            //Assert
            Assert.True(action);
        }
    }
}
