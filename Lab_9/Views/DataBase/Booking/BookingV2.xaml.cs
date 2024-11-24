using Lab_9.ViewModels.DataBase.Booking;

namespace Lab_9.Views.DataBase.Booking;

public partial class BookingV2 : ContentPage
{
    public int Id { get; set; }
    public BookingV2ViewModel ViewModel { get; set; }

    public BookingV2(int id)
	{
		InitializeComponent();

        Id = id;

        ViewModel = new BookingV2ViewModel();
        BindingContext = ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel != null)
        {
            _ = Task.Run(() => ViewModel.LoadDataAsync(Id));
        }
    }
}