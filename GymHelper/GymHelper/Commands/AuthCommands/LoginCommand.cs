using GymHelper.Data.Interfaces;
using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands
{
    class LoginCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly LoginPageVM viewModel;

        public LoginCommand(LoginPageVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            User user = (User)parameter;

            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return true;
        }

        public async void Execute(object parameter)
        {
            await viewModel.LoginTo((User)parameter);
        }
    }
}
