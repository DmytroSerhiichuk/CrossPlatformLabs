using Lab_9.DTO;
using Lab_9.Services;
using Lab_9.Views.DataBase.BookingStatus;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lab_9.ViewModels.DataBase.BookingStatus
{
    public class BookingStatusesViewModel : BaseViewModel
    {
        private bool _isBusy;
        private bool _isLoaded;
        private ObservableCollection<BookingStatusDTO> _bookingStatuses;

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
        public ObservableCollection<BookingStatusDTO> BookingStatuses
        {
            get => _bookingStatuses;
            set
            {
                if (_bookingStatuses != value)
                {
                    _bookingStatuses = value;
                    OnPropertyChanged(nameof(BookingStatuses));
                }
            }
        }

        public ICommand ShowMoreCommand { get; }

        public BookingStatusesViewModel()
        {
            ShowMoreCommand = new Command<string>(OnShowMore);
        }

        public async Task LoadDataAsync()
        {
            IsBusy = true;
            IsLoaded = false;

            try
            {
                var token = await AuthService.GetTokenAsync();

                var data = await Lab6Service.GetData<ObservableCollection<BookingStatusDTO>>(token, "booking-status");

                BookingStatuses = data;
            }
            catch (UnauthorizedAccessException)
            {
                AuthService.RemoveToken();
                (App.Current.MainPage as AppShell)?.UpdateNavigation(false);
                await Shell.Current.GoToAsync("//login");
            }
            catch (HttpRequestException)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong", "OK");
            }
            finally
            {
                IsBusy = false;
                IsLoaded = true;
            }
        }
        public async void OnShowMore(string code)
        {
            await Shell.Current.Navigation.PushAsync(new BookingStatusView(code));
        }
    }
}
