using Lab_9.ViewModels.DataBase.Customer;

namespace Lab_9.Views.DataBase.Customer;

public partial class CustomersView : ContentPage
{
    public CustomersViewModel ViewModel { get; set; }
    public CustomersView()
	{
		InitializeComponent();
        ViewModel = new CustomersViewModel();
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