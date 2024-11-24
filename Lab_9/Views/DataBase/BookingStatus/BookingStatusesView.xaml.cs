using Lab_9.ViewModels.DataBase.BookingStatus;

namespace Lab_9.Views.DataBase.BookingStatus;

public partial class BookingStatusesView : ContentPage
{
    public BookingStatusesViewModel ViewModel { get; set; }
    public BookingStatusesView()
	{
		InitializeComponent();
        ViewModel = new BookingStatusesViewModel();
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