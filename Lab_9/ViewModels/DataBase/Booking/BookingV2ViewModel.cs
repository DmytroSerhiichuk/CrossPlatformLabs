using Lab_9.DTO;
using Lab_9.Services;
using Lab_9.Views.DataBase.Booking;
using System.Windows.Input;

namespace Lab_9.ViewModels.DataBase.Booking
{
    public class BookingV2ViewModel : BaseViewModel
    {
        private bool _isBusy;
        private bool _isLoaded;
        private BookingDetailedDTO _booking;

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
        public BookingDetailedDTO Booking
        {
            get => _booking;
            set
            {
                if (_booking != value)
                {
                    _booking = value;
                    OnPropertyChanged(nameof(Booking));
                }
            }
        }

        public ICommand ShowOldApiCommand { get; set; }
        public BookingV2ViewModel()
        {
            ShowOldApiCommand = new Command(ShowOldApi);
        }

        public async Task LoadDataAsync(int id)
        {
            IsBusy = true;
            IsLoaded = false;

            try
            {
                var token = await AuthService.GetTokenAsync();

                var data = await Lab6Service.GetData<BookingDetailedDTO>(token, $"v2.0/booking/{id}");

                Booking = data;
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
        public async void ShowOldApi()
        {
            await Shell.Current.Navigation.PushAsync(new BookingV1(Booking.Id));
            Shell.Current.Navigation.RemovePage(Shell.Current.Navigation.NavigationStack[Shell.Current.Navigation.NavigationStack.Count - 2]);
        }
    }
}
