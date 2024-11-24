using Lab_9.Views.DataBase.Booking;
using Lab_9.Views.DataBase.BookingStatus;
using Lab_9.Views.DataBase.Customer;

namespace Lab_9
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("database/bookings", typeof(BookingsView));
            Routing.RegisterRoute("database/booking-statuses", typeof(BookingStatusesView));
            Routing.RegisterRoute("database/customers", typeof(CustomersView));
        }

        public void UpdateNavigation(bool isLoggedIn)
        {
            this.FindByName<ShellContent>("ProfileShell").FlyoutItemIsVisible = isLoggedIn;
            this.FindByName<ShellContent>("LoginShell").FlyoutItemIsVisible = !isLoggedIn;
            this.FindByName<ShellContent>("SignUpShell").FlyoutItemIsVisible = !isLoggedIn;
        }
    }
}
