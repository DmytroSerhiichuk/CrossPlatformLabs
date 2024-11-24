namespace Lab_9
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Idiom == DeviceIdiom.Watch)
            {
                TitleLabel.Style = null;
                TitleLabel.FontAttributes = FontAttributes.Bold;
            }
        }
    }

}
