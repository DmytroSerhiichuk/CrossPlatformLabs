using Lab_9.ViewModels.DataBase.Booking;

namespace Lab_9.Views.DataBase.Booking;

public partial class BookingV2 : ContentPage
{
    public int BookingId { get; set; }
    public BookingV2ViewModel ViewModel { get; set; }

    public BookingV2(int id)
	{
		InitializeComponent();

        BookingId = id;

        ViewModel = new BookingV2ViewModel();
        BindingContext = ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel != null)
        {
            _ = Task.Run(() => ViewModel.LoadDataAsync(BookingId));
        }
    }
}