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

namespace SalonApp.WPF;

public partial class MainWindow : Window
{
    private readonly ClientsViewModel _clientsViewModel;
    private readonly ServicesViewModel _servicesViewModel;

    public MainWindow(ClientsViewModel clientsViewModel, ServicesViewModel servicesViewModel)
    {
        InitializeComponent();
        _clientsViewModel = clientsViewModel;
        _servicesViewModel = servicesViewModel;

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

    private void ShowClients()
    {
        var view = new ClientsView();
        view.DataContext = _clientsViewModel;
        MainContent.Content = view;
    }
}