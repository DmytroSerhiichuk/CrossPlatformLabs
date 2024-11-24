using Lab_9.DTO;
using Lab_9.Services;
using Microcharts;
using SkiaSharp;

namespace Lab_9.ViewModels.DataBase
{
    public class GraphicViewModel : BaseViewModel
    {
        private bool _isBusy;
        private bool _isLoaded;
        private Chart _chartData;

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

        public Chart ChartData
        {
            get => _chartData;
            set
            {
                if (_chartData != value)
                {
                    _chartData = value;
                    OnPropertyChanged(nameof(ChartData));
                }
            }
        }

        public async Task LoadDataAsync()
        {
            IsBusy = true;
            IsLoaded = false;

            try
            {
                var token = await AuthService.GetTokenAsync();

                var data = await Lab6Service.GetData<List<BookingStatusDTO>>(token, "booking-status");

                var entries = data.Select(status =>
                    new ChartEntry(status.BookingCount)
                    {
                        Label = status.Code,
                        ValueLabel = status.BookingCount.ToString(),
                        Color = SKColor.Parse("#2c3e50")
                    }).ToList();

                ChartData = new BarChart
                {
                    Entries = entries,
                    LabelTextSize = 30
                };
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
    }
}
