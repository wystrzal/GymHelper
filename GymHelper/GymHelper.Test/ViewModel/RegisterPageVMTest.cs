using GymHelper.Data.Interfaces;
using GymHelper.Models;
using GymHelper.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GymHelper.Test.ViewModel
{
    public class RegisterPageVMTest
    {
        private readonly Mock<IAuthService> authService;
        private readonly Mock<INavigateService> navigateService;
        private readonly User user;
        private readonly RegisterPageVM vm;

        public RegisterPageVMTest()
        {
            authService = new Mock<IAuthService>();
            navigateService = new Mock<INavigateService>();
            App.Data.AuthService = authService.Object;
            App.Data.NavigateService = navigateService.Object;
            user = new User { Login = "test", Password = "Test123", RepeatPassword = "Test123" };
            vm = new RegisterPageVM();
        }

        [Fact]
        public async Task Register_Success()
        {
            //Arrange
            authService.Setup(x => x.Register(user)).Returns(Task.FromResult(true));

            //Act
            await vm.Register(user);

            //Assert
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public async Task Register_Failed()
        {
            //Arrange
            authService.Setup(x => x.Register(user)).Returns(Task.FromResult(false));

            //Act
            await vm.Register(user);

            //Assert
            navigateService.Verify(x => x.NavigateBack(), Times.Never);
        }

        [Fact]
        public void CancelCommand_Success()
        {
            //Act
            vm.Cancel.Execute(null);

            //Assert
            navigateService.Verify(x => x.NavigateBack(), Times.Once);
        }

        [Fact]
        public void RegisterCommand_CanExecute_NullUser_Failed()
        {
            //Act
            var action = vm.RegisterCommand.CanExecute(null);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void RegisterCommand_CanExecute_RepeatPasswordNotEqualPassword_Failed()
        {
            //Arrange
            user.RepeatPassword = "Test1234";

            //Act
            var action = vm.RegisterCommand.CanExecute(user);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void RegisterCommand_CanExecute_EmptyString_Failed()
        {
            //Arrange
            user.Login = "";

            //Act
            var action = vm.RegisterCommand.CanExecute(user);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void RegisterCommand_CanExecute_Success()
        {
            //Act
            var action = vm.RegisterCommand.CanExecute(user);

            //Assert
            Assert.True(action);
        }
    }
}
