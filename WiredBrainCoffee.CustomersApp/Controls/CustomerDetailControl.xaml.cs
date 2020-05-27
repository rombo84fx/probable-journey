using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WiredBrainCoffee.CustomersApp.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace WiredBrainCoffee.CustomersApp.Controls
{
    public sealed partial class CustomerDetailControl : UserControl
    {
        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                FirstNameTextBox.Text = _customer?.FirstName ?? string.Empty;
                LastNameTextBox.Text = _customer?.LastName ?? string.Empty;
                IsDeveloperCheckBox.IsChecked = _customer?.IsDeveloper;
            }
        }

        public CustomerDetailControl()
        {
            this.InitializeComponent();
        }

        private void FirstNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCustomer();
        }

        private void LastNameTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCustomer();
        }

        private void IsDeveloperCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateCustomer();
        }

        private void IsDeveloperCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateCustomer();
        }

        private void UpdateCustomer()
        {
            if (Customer == null) return;
            Customer.FirstName = FirstNameTextBox.Text;
            Customer.LastName = LastNameTextBox.Text;
            Customer.IsDeveloper = IsDeveloperCheckBox.IsChecked.GetValueOrDefault();
        }
    }
}
