using Lab_9.DTO;
using Lab_9.Services;
using Lab_9.Views.DataBase.Booking;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lab_9.ViewModels.DataBase.Booking
{
    public class BookingsViewModel : BaseViewModel
    {
        private bool _isBusy;
        private bool _isLoaded;
        private ObservableCollection<BookingDTO> _bookings;

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
        public ObservableCollection<BookingDTO> Bookings
        {
            get => _bookings;
            set
            {
                if (_bookings != value)
                {
                    _bookings = value;
                    OnPropertyChanged(nameof(Bookings));
                }
            }
        }

        public ICommand ShowMoreCommand { get; }

        public BookingsViewModel()
        {
            ShowMoreCommand = new Command<int>(OnShowMore);
        }

        public async Task LoadDataAsync()
        {
            IsBusy = true;
            IsLoaded = false;

            try
            {
                var token = await AuthService.GetTokenAsync();

                var data = await Lab6Service.GetData<ObservableCollection<BookingDTO>>(token, "booking");

                Bookings = data;
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
        public async void OnShowMore(int id)
        {
            await Shell.Current.Navigation.PushAsync(new BookingV2(id));
        }
    }
}
