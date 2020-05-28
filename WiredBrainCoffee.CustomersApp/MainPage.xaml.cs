using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WiredBrainCoffee.CustomersApp.DataProvider;
using WiredBrainCoffee.CustomersApp.Model;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(new CustomerDataProvider());
            DataContext = ViewModel;
            Loaded += MainPage_OnLoaded;
            Application.Current.Suspending += Application_OnSuspending;
            RequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark
                ? ElementTheme.Dark
                : ElementTheme.Light;
        }

        public MainViewModel ViewModel { get; }

        private async void Application_OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            await ViewModel.SaveAsync();
            deferral.Complete();
        }

        private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadAsync();
        }

        private void ButtonMoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            int currentColumn = Grid.GetColumn(CustomerListGrid);
            int newColumn = currentColumn == 0 ? 2 : 0;
            Grid.SetColumn(CustomerListGrid, newColumn);
            MoveSymbolIcon.Symbol = currentColumn == 0 ? Symbol.Back : Symbol.Forward;
        }

        private void ButtonToggleThem_OnClick(object sender, RoutedEventArgs e)
        {
            RequestedTheme = RequestedTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;
        }
    }
}