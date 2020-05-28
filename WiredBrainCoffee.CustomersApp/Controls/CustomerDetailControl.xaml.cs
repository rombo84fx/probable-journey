using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WiredBrainCoffee.CustomersApp.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace WiredBrainCoffee.CustomersApp.Controls
{
    [ContentProperty(Name = nameof(Model.Customer))]
    public sealed partial class CustomerDetailControl : UserControl
    {
        public static readonly DependencyProperty CustomerProperty = DependencyProperty.Register(
            "Customer", typeof(Customer), typeof(CustomerDetailControl),
            new PropertyMetadata(default(Customer)));

        public Customer Customer
        {
            get => (Customer)GetValue(CustomerProperty);
            set => SetValue(CustomerProperty, value);
        }

        public CustomerDetailControl()
        {
            this.InitializeComponent();
        }
    }
}
