using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WiredBrainCoffee.CustomersApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Customer added!");
            await messageDialog.ShowAsync();
        }

        private void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonMoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            int currentColumn = Grid.GetColumn(CustomerListGrid);
            int newColumn = currentColumn == 0 ? 2 : 0;
            Grid.SetColumn(CustomerListGrid, newColumn);
            MoveSymbolIcon.Symbol = currentColumn == 0 ? Symbol.Back : Symbol.Forward; 
        }
    }
}