using Lab_9.CreateDTO;
using Lab_9.Services;
using System.Windows.Input;

namespace Lab_9.ViewModels.DataBase.Customer
{
    public class CustomerCreateViewModel : BaseViewModel
    {
        private CustomerCreateDTO _model;

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
        public string Name
        {
            get => _model.Name;
            set
            {
                if (_model.Name != value)
                {
                    _model.Name = value;
                    OnPropertyChanged(nameof(Name));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string Details
        {
            get => _model.Details;
            set
            {
                if (_model.Details != value)
                {
                    _model.Details = value;
                    OnPropertyChanged(nameof(Details));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string Gender
        {
            get => _model.Gender;
            set
            {
                if (_model.Gender != value)
                {
                    _model.Gender = value;
                    OnPropertyChanged(nameof(Gender));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string Email
        {
            get => _model.Email;
            set
            {
                if (_model.Email != value)
                {
                    _model.Email = value;
                    OnPropertyChanged(nameof(Email));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string Phone
        {
            get => _model.Phone;
            set
            {
                if (_model.Phone != value)
                {
                    _model.Phone = value;
                    OnPropertyChanged(nameof(Phone));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string AddressLine1
        {
            get => _model.AddressLine1;
            set
            {
                if (_model.AddressLine1 != value)
                {
                    _model.AddressLine1 = value;
                    OnPropertyChanged(nameof(AddressLine1));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string AddressLine2
        {
            get => _model.AddressLine2;
            set
            {
                if (_model.AddressLine2 != value)
                {
                    _model.AddressLine2 = value;
                    OnPropertyChanged(nameof(AddressLine2));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string AddressLine3
        {
            get => _model.AddressLine3;
            set
            {
                if (_model.AddressLine3 != value)
                {
                    _model.AddressLine3 = value;
                    OnPropertyChanged(nameof(AddressLine3));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string Town
        {
            get => _model.Town;
            set
            {
                if (_model.Town != value)
                {
                    _model.Town = value;
                    OnPropertyChanged(nameof(Town));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string County
        {
            get => _model.County;
            set
            {
                if (_model.County != value)
                {
                    _model.County = value;
                    OnPropertyChanged(nameof(County));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }
        public string Country
        {
            get => _model.Country;
            set
            {
                if (_model.Country != value)
                {
                    _model.Country = value;
                    OnPropertyChanged(nameof(Country));
                    UpdateCanCreate();
                    IsInvalidData = false;
                }
            }
        }

        public ICommand CreateCommand { get; }
        public CustomerCreateViewModel()
        {
            CreateCommand = new Command(OnCreate, () => CanCreate);

            _model = new CustomerCreateDTO();
        }


        private void UpdateCanCreate()
        {
            CanCreate = !IsBusy && 
                Id >= 0 &&
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Gender) &&
                !string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(Phone) &&
                !string.IsNullOrEmpty(AddressLine1) &&
                !string.IsNullOrEmpty(Town) &&
                !string.IsNullOrEmpty(County) &&
                !string.IsNullOrEmpty(Country);
        }

        public async void OnCreate()
        {
            if (IsBusy || !CanCreate)
                return;

            IsBusy = true;

            try
            {
                var token = await AuthService.GetTokenAsync();

                await Lab6Service.PostData(token, "customer", _model);

                await App.Current.MainPage.DisplayAlert("Success", "New customer successfully created", "OK");
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
