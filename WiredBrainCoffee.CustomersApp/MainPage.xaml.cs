using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Popups;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WiredBrainCoffee.CustomersApp.DataProvider;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp
{
    public sealed partial class MainPage : Page
    {
        private readonly CustomerDataProvider _customerDataProvider;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_OnLoaded;
            Application.Current.Suspending += AppOnSuspending;
             _customerDataProvider = new CustomerDataProvider();
             RequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark
                 ? ElementTheme.Dark : ElementTheme.Light;
        }

        private async void AppOnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            await _customerDataProvider.SaveCustomersAsync(
                CustomerListView.Items.OfType<Customer>());
            deferral.Complete();
        }

        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            CustomerListView.Items.Clear();
            IEnumerable<Customer> customers = await _customerDataProvider.LoadCustomersAsync();
            foreach (Customer customer in customers)
            {
                CustomerListView.Items.Add(customer);
            }
        }

        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer{ FirstName = "New" };
            CustomerListView.Items.Add(customer);
            CustomerListView.SelectedItem = customer;
        }

        private void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerListView.SelectedItem is Customer customer)
            {
                CustomerListView.Items.Remove(customer);
            }
        }

        private void ButtonMoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            int currentColumn = Grid.GetColumn(CustomerListGrid);
            int newColumn = currentColumn == 0 ? 2 : 0;
            Grid.SetColumn(CustomerListGrid, newColumn);
            MoveSymbolIcon.Symbol = currentColumn == 0 ? Symbol.Back : Symbol.Forward; 
        }

        private void CustomerListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = CustomerListView.SelectedItem as Customer;
            CustomerDetailControl.Customer = customer;
        }

        private void ButtonToggleThem_OnClick(object sender, RoutedEventArgs e)
        {
            RequestedTheme = RequestedTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;
        }
    }
}