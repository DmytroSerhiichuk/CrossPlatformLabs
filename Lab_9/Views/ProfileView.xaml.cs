using Lab_9.ViewModels;

namespace Lab_9.Views;

public partial class ProfileView : ContentPage
{
    public ProfileViewModel ViewModel;

    public ProfileView()
	{
		InitializeComponent();
        ViewModel = new ProfileViewModel();
        BindingContext = ViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel != null)
        {
            await ViewModel.LoadUserAsync();
        }
    }
}