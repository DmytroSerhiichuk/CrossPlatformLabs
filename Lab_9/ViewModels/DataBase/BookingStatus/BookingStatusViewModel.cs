using Lab_9.DTO;
using Lab_9.Services;

namespace Lab_9.ViewModels.DataBase.BookingStatus
{
    public class BookingStatusViewModel : BaseViewModel
    {
        private bool _isBusy;
        private bool _isLoaded;
        private BookingStatusDetailedDTO _bookingStatus;

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
        public BookingStatusDetailedDTO BookingStatus
        {
            get => _bookingStatus;
            set
            {
                if (_bookingStatus != value)
                {
                    _bookingStatus = value;
                    OnPropertyChanged(nameof(BookingStatus));
                }
            }
        }

        public async Task LoadDataAsync(string code)
        {
            IsBusy = true;
            IsLoaded = false;

            try
            {
                var token = await AuthService.GetTokenAsync();

                var data = await Lab6Service.GetData<BookingStatusDetailedDTO>(token, $"booking-status/{code}");

                BookingStatus = data;
            }
            catch (UnauthorizedAccessException)
            {
                AuthService.RemoveToken();
                (App.Current.MainPage as AppShell)?.UpdateNavigation(false);
                await Shell.Current.GoToAsync("//login");
            }
            catch (HttpRequestException ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
            }
            finally
            {
                IsBusy = false;
                IsLoaded = true;
            }
        }
    }
}
