﻿using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using GymHelper.Models;
using GymHelper.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data
{
    class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAlertService alertService;

        public AuthService(IUnitOfWork unitOfWork, IAlertService alertService)
        {
            this.unitOfWork = unitOfWork;
            this.alertService = alertService;
        }

        public async Task<bool> LoginTo(string username, string password)
        {
            try
            {
                var user = await unitOfWork.Repository<User>().ReadFirstByCondition(u => u.Login == username);

                if (user != null)
                {
                    if (user.Login == username && user.Password == password)
                    {
                        App.User = user;
                        return true;
                    }

                    await alertService.DisplayAlert("Niepowodzenie", "Nieprawidłowy login lub hasło!", "Ok");
                    return false;
                }

                await alertService.DisplayAlert("Niepowodzenie", "Nieprawidłowy login lub hasło!", "Ok");
                return false;
            }
            catch (Exception)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nie udało się zalogować!", "Ok");
                return false;
            }
        }

        public async Task<bool> Register(User user)
        {
            if (user.Password.Length < 6)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Hasło powinno zawierać minimum 6 znaków.", "Ok");
                return false;
            }

            if (!user.Password.ContainsDigit())
            {
                await alertService.DisplayAlert("Niepowodzenie", "Hasło powinno zawierać minimum 1 liczbę.", "Ok");
                return false;
            }

            if (!user.Password.ContainsUpper())
            {
                await alertService.DisplayAlert("Niepowodzenie", "Hasło powinno zawierać minimum 1 dużą literę.", "Ok");
                return false;
            }

            bool userExist = await unitOfWork.Repository<User>().CheckIfExistByCondition(x => x.Login == user.Login);
            if (userExist)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Wybrany login jest zajęty.", "Ok");
                return false;
            }

            await unitOfWork.Repository<User>().Add(user);
            return true;
        }
    }
}
