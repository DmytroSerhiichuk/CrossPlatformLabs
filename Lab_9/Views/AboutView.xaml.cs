namespace Lab_9.Views;

public partial class AboutView : ContentPage
{
	public AboutView()
	{
		InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Idiom == DeviceIdiom.Watch)
        {
            TitleLabel.Style = null;
            TitleLabel.FontAttributes = FontAttributes.Bold;
        }
    }
}