using SalonApp.WPF.ViewModels;
using SalonApp.WPF.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SalonApp.WPF.Views;
namespace SalonApp.WPF;

public partial class MainWindow : Window
{
    private readonly ClientsViewModel _clientsViewModel;
    private readonly ServicesViewModel _servicesViewModel;
    private readonly StaffViewModel _staffViewModel;
    private readonly AppointmentsViewModel _appointmentsViewModel;
    private readonly OverviewViewModel _overviewViewModel;
    private readonly NotificationsViewModel _notificationsViewModel;

    public MainWindow(ClientsViewModel clientsViewModel, ServicesViewModel servicesViewModel,
    StaffViewModel staffViewModel, AppointmentsViewModel appointmentsViewModel,
    OverviewViewModel overviewViewModel, NotificationsViewModel notificationsViewModel)
    {
        InitializeComponent();
        _clientsViewModel = clientsViewModel;
        _servicesViewModel = servicesViewModel;
        _staffViewModel = staffViewModel;
        _appointmentsViewModel = appointmentsViewModel;
        _overviewViewModel = overviewViewModel;
        _notificationsViewModel = notificationsViewModel;

        ShowClients();
    }

    private void Clients_Click(object sender, RoutedEventArgs e)
    {
        ShowClients();
    }

    private void Services_Click(object sender, RoutedEventArgs e)
    {
        var view = new ServicesView();
        view.DataContext = _servicesViewModel;
        MainContent.Content = view;
    }

    private void Staff_Click(object sender, RoutedEventArgs e)
    {
        var view = new StaffView();
        view.DataContext = _staffViewModel;
        MainContent.Content = view;
    }

    private void ShowClients()
    {
        var view = new ClientsView();
        view.DataContext = _clientsViewModel;
        MainContent.Content = view;
    }

    private async void Appointments_Click(object sender, RoutedEventArgs e)
    {
        var view = new AppointmentsView();
        view.DataContext = _appointmentsViewModel;
        MainContent.Content = view;
        await _appointmentsViewModel.RefreshDataAsync();
    }

    private async void Overview_Click(object sender, RoutedEventArgs e)
    {
        var view = new OverviewView();
        view.DataContext = _overviewViewModel;
        MainContent.Content = view;
        await _overviewViewModel.RefreshDataAsync();
    }

    private async void Notifications_Click(object sender, RoutedEventArgs e)
    {
        var view = new NotificationsView();
        view.DataContext = _notificationsViewModel;
        MainContent.Content = view;
        await _notificationsViewModel.LoadNotificationsAsync();
    }

}