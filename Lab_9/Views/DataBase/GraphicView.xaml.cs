using Lab_9.ViewModels.DataBase;

namespace Lab_9.Views.DataBase;

public partial class GraphicView : ContentPage
{
    public GraphicViewModel ViewModel { get; set; }
    public GraphicView()
	{
		InitializeComponent();

        ViewModel = new GraphicViewModel();
        BindingContext = ViewModel;

        if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Idiom == DeviceIdiom.Watch)
        {
            MainChart.HeightRequest = 120;
        }
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