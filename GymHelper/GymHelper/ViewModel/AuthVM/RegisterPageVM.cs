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
        public BaseCommand RegisterCommand { get; set; }
        public ICommand Cancel => new Command(async () => await NavigateService.NavigateBack());

        private readonly IAuthService authService = App.Data.AuthService;

        public RegisterPageVM()
        {
            RegisterCommand = new RegisterCommand(this);
        }

        private User user = new User();
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                user.Login = login;
                RegisterCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Login");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                user.Password = password;
                RegisterCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Password");
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                repeatPassword = value;
                user.RepeatPassword = repeatPassword;
                RegisterCommand.RaiseCanExecuteChanged();
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