using Lab_9.Models;
using Lab_9.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Lab_9.ViewModels.Account
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private SignUpModel _signUpModel;

        private bool _isBusy;
        private bool _canSignUp;
        private bool _isInvalidData;
        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName
        {
            get => _signUpModel.UserName;
            set
            {
                if (_signUpModel.UserName != value)
                {
                    _signUpModel.UserName = value;
                    OnPropertyChanged(nameof(UserName));
                    UpdateCanSignUp();
                    IsInvalidData = false;
                }
            }
        }
        public string FullName
        {
            get => _signUpModel.FullName;
            set
            {
                if (_signUpModel.FullName != value)
                {
                    _signUpModel.FullName = value;
                    OnPropertyChanged(nameof(FullName));
                    UpdateCanSignUp();
                    IsInvalidData = false;
                }
            }
        }
        public string Email
        {
            get => _signUpModel.Email;
            set
            {
                if (_signUpModel.Email != value)
                {
                    _signUpModel.Email = value;
                    OnPropertyChanged(nameof(Email));
                    UpdateCanSignUp();
                    IsInvalidData = false;
                }
            }
        }
        public string PhoneNumber
        {
            get => _signUpModel.PhoneNumber;
            set
            {
                if (_signUpModel.PhoneNumber != value)
                {
                    _signUpModel.PhoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                    UpdateCanSignUp();
                    IsInvalidData = false;
                }
            }
        }
        public string Password
        {
            get => _signUpModel.Password;
            set
            {
                if (_signUpModel.Password != value)
                {
                    _signUpModel.Password = value;
                    OnPropertyChanged(nameof(Password));
                    UpdateCanSignUp();
                    IsInvalidData = false;
                }
            }
        }
        public string PasswordConfirm
        {
            get => _signUpModel.PasswordConfirm;
            set
            {
                if (_signUpModel.Password != value)
                {
                    _signUpModel.PasswordConfirm = value;
                    OnPropertyChanged(nameof(PasswordConfirm));
                    UpdateCanSignUp();
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
                    UpdateCanSignUp();
                }
            }
        }
        public bool CanSignUp
        {
            get => _canSignUp;
            set
            {
                _canSignUp = value;
                OnPropertyChanged(nameof(CanSignUp));
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

        public ICommand SignUpCommand { get; }

        public SignUpViewModel()
        {
            SignUpCommand = new Command(OnSignUp, () => CanSignUp);

            _signUpModel = new SignUpModel();
        }

        private void UpdateCanSignUp()
        {
            CanSignUp = !IsBusy && 
                !string.IsNullOrEmpty(Email) && 
                !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(UserName) &&
                !string.IsNullOrEmpty(FullName) &&
                !string.IsNullOrEmpty(PasswordConfirm) &&
                !string.IsNullOrEmpty(PhoneNumber);
        }

        public async void OnSignUp()
        {
            if (IsBusy || !CanSignUp)
                return;

            IsBusy = true;

            try
            {
                await AuthService.CreateUserAsync(_signUpModel);

                var token = await AuthService.AuthenticateUserAsync(_signUpModel.Email, _signUpModel.Password);

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


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
