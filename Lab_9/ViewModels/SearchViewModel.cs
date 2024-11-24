using Lab_9.DTO;
using Lab_9.Models;
using Lab_9.Services;
using Lab_9.Views.DataBase.Booking;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lab_9.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private SearchModel _form;
        private ObservableCollection<BookingDTO> _bookings;

        private bool _isBusy;
        private bool _isLoaded;

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

        public DateTime? DateFrom
        {
            get => _form.DateFrom;
            set
            {
                if (_form.DateFrom != value)
                {
                    _form.DateFrom = value;
                    OnPropertyChanged(nameof(DateFrom));
                }
            }
        }
        public DateTime? DateTo
        {
            get => _form.DateTo;
            set
            {
                if (_form.DateTo != value)
                {
                    _form.DateTo = value;
                    OnPropertyChanged(nameof(DateTo));
                }
            }
        }
        public string? Items
        {
            get => _form.Items;
            set
            {
                if (_form.Items != value)
                {
                    _form.Items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }
        public string? StartsWith
        {
            get => _form.StartsWith;
            set
            {
                if (_form.StartsWith != value)
                {
                    _form.StartsWith = value;
                    OnPropertyChanged(nameof(StartsWith));
                }
            }
        }
        public string? EndsWith
        {
            get => _form.EndsWith;
            set
            {
                if (_form.EndsWith != value)
                {
                    _form.EndsWith = value;
                    OnPropertyChanged(nameof(EndsWith));
                }
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ShowMoreCommand { get; }
        public SearchViewModel()
        {
            IsLoaded = false;
            SearchCommand = new Command(OnSearch);
            ShowMoreCommand = new Command<int>(OnShowMore);

            _form = new SearchModel();
        }

        public async void OnSearch()
        {
            if (IsBusy) return;

            IsBusy = true;
            IsLoaded = false;

            try
            {
                var token = await AuthService.GetTokenAsync();

                var query = _form.ParseToQuery();
                Bookings = await Lab6Service.GetData<ObservableCollection<BookingDTO>>(token, $"search?{query}");                
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
        public async void OnShowMore(int id)
        {
            await Shell.Current.Navigation.PushAsync(new BookingV2(id));
        }
    }
}
