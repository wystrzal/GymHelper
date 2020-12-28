using GymHelper.Data.Interfaces;
using GymHelper.Helpers;
using GymHelper.Models;
using GymHelper.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data
{
    public class AuthService : IAuthService
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
            var user = await unitOfWork.Repository<User>()
                .ReadFirstByConditionWithInclude(u => u.Login == username, y => y.Diet);

            if (user == null)
            {
                await alertService.DisplayAlert("Niepowodzenie", "Nieprawidłowy login lub hasło!", "Ok");
                return false;
            }

            if (user.Login == username && user.Password == password)
            {
                App.Data.User = user;
                return true;
            }

            await alertService.DisplayAlert("Niepowodzenie", "Nieprawidłowy login lub hasło!", "Ok");
            return false;
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
            await unitOfWork.Repository<Diet>().Add(new Diet { User = user });
            return await unitOfWork.Repository<User>().SaveChanges();
        }
    }
}
