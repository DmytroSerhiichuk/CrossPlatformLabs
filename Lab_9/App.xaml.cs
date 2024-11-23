using Lab_9.Services;

namespace Lab_9
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            var token = await AuthService.GetTokenAsync();

            if (MainPage is AppShell shell)
            {
                shell.UpdateNavigation(!string.IsNullOrEmpty(token));
            }
        }
    }
}
