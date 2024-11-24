using Lab_9.CreateDTO;
using System.Windows.Input;
using Lab_9.Services;

namespace Lab_9.ViewModels.DataBase.Booking
{
    public class BookingCreateViewModel : BaseViewModel
    {
        private BookingCreateDTO _model;

        private bool _isBusy;
        private bool _canCreate;
        private bool _isInvalidData;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                    UpdateCanCreate();
                }
            }
        }
        public bool CanCreate
        {
            get => _canCreate;
            set
            {
                _canCreate = value;
                OnPropertyChanged(nameof(CanCreate));
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

        public int Id
        {
            get => _model.Id;
            set
            {
                if (_model.Id != value)
                {
                    _model.Id = value;
                    OnPropertyChanged(nameof(Id));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public DateTime DateFrom
        {
            get => _model.DateFrom;
            set
            {
                if (_model.DateFrom != value)
                {
                    _model.DateFrom = value;
                    OnPropertyChanged(nameof(DateFrom));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public DateTime DateTo
        {
            get => _model.DateTo;
            set
            {
                if (_model.DateTo != value)
                {
                    _model.DateTo = value;
                    OnPropertyChanged(nameof(DateTo));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string IsConfirmationLetterSent
        {
            get => _model.IsConfirmationLetterSent;
            set
            {
                if (_model.IsConfirmationLetterSent != value)
                {
                    _model.IsConfirmationLetterSent = value;
                    OnPropertyChanged(nameof(IsConfirmationLetterSent));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string IsPaymentReceived
        {
            get => _model.IsPaymentReceived;
            set
            {
                if (_model.IsPaymentReceived != value)
                {
                    _model.IsPaymentReceived = value;
                    OnPropertyChanged(nameof(IsPaymentReceived));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string BookingStatusCode
        {
            get => _model.BookingStatusCode;
            set
            {
                if (_model.BookingStatusCode != value)
                {
                    _model.BookingStatusCode = value;
                    OnPropertyChanged(nameof(BookingStatusCode));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string VehicleRegNumber
        {
            get => _model.VehicleRegNumber;
            set
            {
                if (_model.VehicleRegNumber != value)
                {
                    _model.VehicleRegNumber = value;
                    OnPropertyChanged(nameof(VehicleRegNumber));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public int CustomerId
        {
            get => _model.CustomerId;
            set
            {
                if (_model.CustomerId != value)
                {
                    _model.CustomerId = value;
                    OnPropertyChanged(nameof(CustomerId));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }

        public ICommand CreateCommand { get; }
        public BookingCreateViewModel()
        {
            CreateCommand = new Command(OnCreate, () => CanCreate);

            _model = new BookingCreateDTO();
        }


        private void UpdateCanCreate()
        {
            CanCreate = !IsBusy &&
                Id >= 0 &&
                !string.IsNullOrEmpty(IsConfirmationLetterSent) &&
                !string.IsNullOrEmpty(IsPaymentReceived) &&
                !string.IsNullOrEmpty(BookingStatusCode) &&
                !string.IsNullOrEmpty(VehicleRegNumber) &&
                CustomerId >= 0;
        }

        public async void OnCreate()
        {
            if (IsBusy || !CanCreate)
                return;

            IsBusy = true;

            try
            {
                var token = await AuthService.GetTokenAsync();

                await Lab6Service.PostData(token, "booking", _model);

                await App.Current.MainPage.DisplayAlert("Success", "New booking successfully created", "OK");
            }
            catch (UnauthorizedAccessException)
            {
                AuthService.RemoveToken();
                (App.Current.MainPage as AppShell)?.UpdateNavigation(false);
                await Shell.Current.GoToAsync("//login");
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
