using Lab_9.CreateDTO;
using Lab_9.Services;
using System.Windows.Input;

namespace Lab_9.ViewModels.DataBase.BookingStatus
{
    public class BookingStatusCreateViewModel : BaseViewModel
    {
        private BookingStatusCreateDTO _model;

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

        public string Code
        {
            get => _model.Code;
            set
            {
                if (_model.Code != value)
                {
                    _model.Code = value;
                    OnPropertyChanged(nameof(Code));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string Description
        {
            get => _model.Description;
            set
            {
                if (_model.Description != value)
                {
                    _model.Description = value;
                    OnPropertyChanged(nameof(Description));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }

        public ICommand CreateCommand { get; }
        public BookingStatusCreateViewModel()
        {
            CreateCommand = new Command(OnCreate, () => CanCreate);

            _model = new BookingStatusCreateDTO();
        }


        private void UpdateCanCreate()
        {
            CanCreate = !IsBusy && !string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(Description);
        }

        public async void OnCreate()
        {
            if (IsBusy || !CanCreate)
                return;

            IsBusy = true;

            try
            {
                var token = await AuthService.GetTokenAsync();

                await Lab6Service.PostData(token, "booking-status", _model);

                await App.Current.MainPage.DisplayAlert("Success", "New booking status successfully created", "OK");
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
