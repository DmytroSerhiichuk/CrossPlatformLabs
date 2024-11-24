using Lab_9.ViewModels.DataBase.BookingStatus;

namespace Lab_9.Views.DataBase.BookingStatus;

public partial class BookingStatusView : ContentPage
{
    public string Code { get; set; }
    public BookingStatusViewModel ViewModel { get; set; }
    public BookingStatusView(string code)
	{
		InitializeComponent();

        Code = code;

        ViewModel = new BookingStatusViewModel();
        BindingContext = ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel != null)
        {
            _ = Task.Run(() => ViewModel.LoadDataAsync(Code));
        }
    }
}