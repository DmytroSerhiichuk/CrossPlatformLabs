using Lab_9.DTO;
using Lab_9.Services;
using Lab_9.Views.DataBase.Customer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Lab_9.ViewModels.DataBase.Customer
{
    public class CustomersViewModel : BaseViewModel
    {
        private bool _isBusy;
        private bool _isLoaded;
        private ObservableCollection<CustomerDTO> _customers;

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
        public ObservableCollection<CustomerDTO> Customers
        {
            get => _customers;
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    OnPropertyChanged(nameof(Customers));
                }
            }
        }

        public ICommand ShowMoreCommand { get; }

        public CustomersViewModel()
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

                var data = await Lab6Service.GetData<ObservableCollection<CustomerDTO>>(token, "customer");

                Customers = data;
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
            await Shell.Current.Navigation.PushAsync(new CustomerView(id));
        }
    }
}
