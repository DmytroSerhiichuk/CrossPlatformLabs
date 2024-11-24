using Lab_9.ViewModels.DataBase.Customer;

namespace Lab_9.Views.DataBase.Customer;

public partial class CustomerView : ContentPage
{
    public int CustomerId { get; set; }
    public CustomerViewModel ViewModel { get; set; }
    public CustomerView(int id)
	{
		InitializeComponent();

        CustomerId = id;

        ViewModel = new CustomerViewModel();
        BindingContext = ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel != null)
        {
            _ = Task.Run(() => ViewModel.LoadDataAsync(CustomerId));
        }
    }
}