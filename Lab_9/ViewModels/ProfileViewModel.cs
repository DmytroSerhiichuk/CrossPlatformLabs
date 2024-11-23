using Lab_9.Models;
using Lab_9.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Lab_9.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private UserModel _user;
        private bool _isBusy;
        private bool _isLoaded;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserModel User 
        { 
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
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
                }
            }
        }
        public bool IsLoaded
        {
            get => _isLoaded;
            set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    OnPropertyChanged(nameof(IsLoaded));
                }
            }
        }

        public ICommand LogoutCommand { get; }
        public ProfileViewModel()
        {
            LogoutCommand = new Command(LogoutAsync);
        }

        public async Task LoadUserAsync()
        {
            IsLoaded = false;
            IsBusy = true;
            try
            {
                var token = await AuthService.GetTokenAsync();
                User = await AuthService.GetUserInfoAsync(token);
            }
            catch (Exception)
            {
                AuthService.RemoveToken();
                (App.Current.MainPage as AppShell)?.UpdateNavigation(false);
                await Shell.Current.GoToAsync("//login");
            }
            finally
            {
                IsBusy = false;
                IsLoaded = true;
            }
        }

        public async void LogoutAsync()
        {
            AuthService.RemoveToken();
            (App.Current.MainPage as AppShell)?.UpdateNavigation(false);
            await Shell.Current.GoToAsync("//login");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
