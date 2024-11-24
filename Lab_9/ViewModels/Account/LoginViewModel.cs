using Lab_9.Models;
using Lab_9.Services;
using System.Windows.Input;

namespace Lab_9.ViewModels.Account
{
    internal class LoginViewModel : BaseViewModel
    {
        private readonly LoginModel _loginModel;

        private bool _isBusy;
        private bool _canLogin;
        private bool _isInvalidData;

        public string Email
        {
            get => _loginModel.Email;
            set
            {
                if (_loginModel.Email != value)
                {
                    _loginModel.Email = value;
                    OnPropertyChanged(nameof(Email));
                    UpdateCanLogin();
                    IsInvalidData = false;
                }
            }
        }
        public string Password
        {
            get => _loginModel.Password;
            set
            {
                if (_loginModel.Password != value)
                {
                    _loginModel.Password = value;
                    OnPropertyChanged(nameof(Password));
                    UpdateCanLogin();
                    IsInvalidData = false;
                }
            }
        }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                    UpdateCanLogin();
                }
            }
        }
        public bool CanLogin
        {
            get => _canLogin;
            set
            {
                _canLogin = value;
                OnPropertyChanged(nameof(CanLogin));
            }
        }
        public bool IsInvalidData
        {
            get => _isInvalidData;
            set 
            {
                if (_isInvalidData != value)
                {
                    _isInvalidData = value;
                    OnPropertyChanged(nameof(IsInvalidData));
                }
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin, () => CanLogin);

            _loginModel = new LoginModel();
        }

        private void UpdateCanLogin()
        {
            CanLogin = !IsBusy && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private async void OnLogin()
        {
            if (IsBusy || !CanLogin)
                return;

            IsBusy = true;

            try
            {
                var token = await AuthService.AuthenticateUserAsync(_loginModel.Email, _loginModel.Password);
                
                if (String.IsNullOrEmpty(token)) throw new Exception();

                await AuthService.SaveTokenAsync(token);

                (App.Current.MainPage as AppShell)?.UpdateNavigation(true);

                await Shell.Current.GoToAsync("//profile");                
            }
            catch (HttpRequestException)
            {                
                IsInvalidData = true;
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
