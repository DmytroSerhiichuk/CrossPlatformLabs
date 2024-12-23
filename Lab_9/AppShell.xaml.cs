﻿using Lab_9.Views.DataBase;
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
            Routing.RegisterRoute("database/booking/create", typeof(BookingCreateView));
            Routing.RegisterRoute("database/booking-statuses", typeof(BookingStatusesView));
            Routing.RegisterRoute("database/booking-status/create", typeof(BookingStatusCreateView));
            Routing.RegisterRoute("database/customers", typeof(CustomersView));
            Routing.RegisterRoute("database/customer/create", typeof(CustomerCreateView));

            Routing.RegisterRoute("database/graphic", typeof(GraphicView));
        }

        public void UpdateNavigation(bool isLoggedIn)
        {
            ProfileShell.FlyoutItemIsVisible = isLoggedIn;
            SearchShell.FlyoutItemIsVisible = isLoggedIn;
            LoginShell.FlyoutItemIsVisible = !isLoggedIn;
            SignUpShell.FlyoutItemIsVisible = !isLoggedIn;
        }
    }
}
