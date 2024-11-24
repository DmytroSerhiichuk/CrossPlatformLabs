using Lab_9.ViewModels.DataBase.Customer;

namespace Lab_9.Views.DataBase.Customer;

public partial class CustomerView : ContentPage
{
    public int Id { get; set; }
    public CustomerViewModel ViewModel { get; set; }
    public CustomerView(int id)
	{
		InitializeComponent();

        Id = id;

        ViewModel = new CustomerViewModel();
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