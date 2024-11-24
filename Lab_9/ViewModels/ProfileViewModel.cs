using Lab_9.Models;
using Lab_9.Services;
using System.Windows.Input;

namespace Lab_9.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private UserModel _user;
        private bool _isBusy;
        private bool _isLoaded;

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
        public ICommand NavigateToBookingsCommand { get; }
        public ICommand NavigateToBookingCreateFormCommand { get; }
        public ICommand NavigateToBookingStatusesCommand { get; }
        public ICommand NavigateToBookingStatusCreateFormCommand { get; }
        public ICommand NavigateToCustomersCommand { get; }
        public ICommand NavigateToCustomerCreateFormCommand { get; }
        public ICommand NavigateToGraphicCommand { get; }
        public ProfileViewModel()
        {
            LogoutCommand = new Command(LogoutAsync);

            NavigateToBookingsCommand = new Command(NavigateToBookings);
            NavigateToBookingCreateFormCommand = new Command(NavigateToBookingCreateForm);

            NavigateToBookingStatusesCommand = new Command(NavigateToBookingStatuses);
            NavigateToBookingStatusCreateFormCommand = new Command(NavigateToBookingStatusCreateForm);

            NavigateToCustomersCommand = new Command(NavigateToCustomers);
            NavigateToCustomerCreateFormCommand = new Command(NavigateToCustomerCreateForm);

            NavigateToGraphicCommand = new Command(NavigateToGraphic);
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

        public async void NavigateToBookings()
        {
            await Shell.Current.GoToAsync("database/bookings");
        }
        public async void NavigateToBookingCreateForm()
        {
            await Shell.Current.GoToAsync("database/booking/create");
        }

        public async void NavigateToBookingStatuses()
        {
            await Shell.Current.GoToAsync("database/booking-statuses");
        }
        public async void NavigateToBookingStatusCreateForm()
        {
            await Shell.Current.GoToAsync("database/booking-status/create");
        }

        public async void NavigateToCustomers()
        {
            await Shell.Current.GoToAsync("database/customers");
        }
        public async void NavigateToCustomerCreateForm()
        {
            await Shell.Current.GoToAsync("database/customer/create");
        }

        public async void NavigateToGraphic()
        {
            await Shell.Current.GoToAsync("database/graphic");
        }
    }
}
