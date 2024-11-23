namespace Lab_9
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public void UpdateNavigation(bool isLoggedIn)
        {
            this.FindByName<ShellContent>("ProfileShell").FlyoutItemIsVisible = isLoggedIn;
            this.FindByName<ShellContent>("LoginShell").FlyoutItemIsVisible = !isLoggedIn;
            this.FindByName<ShellContent>("SignUpShell").FlyoutItemIsVisible = !isLoggedIn;
        }
    }
}
