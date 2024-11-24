using Lab_9.ViewModels.DataBase.Booking;

namespace Lab_9.Views.DataBase.Booking;

public partial class BookingsView : ContentPage
{
    public BookingsViewModel ViewModel { get; set; }

	public BookingsView()
	{
		InitializeComponent();
        ViewModel = new BookingsViewModel();
        BindingContext = ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel != null)
        {
            _ = Task.Run(ViewModel.LoadDataAsync);
        }
    }
}