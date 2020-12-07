using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using GymHelper.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class RegisterPageVM : BaseViewModel
    {
        public RegisterCommand RegisterCommand { get; set; }
        public ICommand Cancel => new Command(async () => await NavigateService.NavigateBack());

        private readonly IAuthService authService;

        public RegisterPageVM()
        {
            authService = App.AuthService;
            RegisterCommand = new RegisterCommand(this);
        }

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                User = new User()
                {
                    Login = Login,
                    Password = Password,
                    RepeatPassword = RepeatPassword
                };
                OnPropertyChanged("Login");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User()
                {
                    Login = Login,
                    Password = Password,
                    RepeatPassword = RepeatPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get { return repeatPassword; }
            set
            {
                repeatPassword = value;
                User = new User()
                {
                    Login = Login,
                    Password = Password,
                    RepeatPassword = RepeatPassword
                };
                OnPropertyChanged("RepeatPassword");
            }
        }

        public async Task Register(User user)
        {
            var registeredSuccessfully = await authService.Register(user);

            if (registeredSuccessfully)
            {
                await NavigateService.NavigateBack();
            }
        }
    }
}
