using GymHelper.Data;
using GymHelper.Data.Interfaces;
using GymHelper.Data.Services;
using GymHelper.Models;
using GymHelper.View;
using GymHelper.ViewModel.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymHelper.ViewModel
{
    public class LoginPageVM : BaseViewModel
    {
        public BaseCommand LoginCommand { get; set; }
        public ICommand RegisterNavCommand => new Command(async () => await NavigateService.Navigate<RegisterPage>());
        private readonly IAuthService authService;

        public LoginPageVM()
        {
            authService = App.Data.AuthService;
            LoginCommand = new LoginCommand(this);
            user = new User();
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
                user.Login = login;
                LoginCommand.RaiseCanExecuteChanged();
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
                user.Password = password;
                LoginCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Password");
            }
        }
        
        public async Task LoginTo(User user)
        {
            var loggedSuccessfully = await authService.LoginTo(user.Login, user.Password);

            if (loggedSuccessfully)
            {
                await NavigateService.Navigate<HomePage>();
            }
        }
    }
}
