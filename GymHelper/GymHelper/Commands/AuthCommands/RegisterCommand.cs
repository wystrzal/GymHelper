﻿using GymHelper.Data.Interfaces;
using GymHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymHelper.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly RegisterPageVM viewModel;

        public RegisterCommand(RegisterPageVM viewModel)
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

            if (user.Password != user.RepeatPassword)
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
            await viewModel.Register((User)parameter);
        }
    }
}