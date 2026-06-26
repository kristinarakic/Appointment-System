using SalonApp.WPF.ViewModels;
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
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void AddClient_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ClientsViewModel vm)
        {
            await vm.AddClientAsync();
        }
    }

    private async void DeleteClient_Click(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button button &&
            button.Tag is SalonApp.Modules.Clients.Domain.Client client &&
            DataContext is ClientsViewModel vm)
        {
            await vm.DeleteClientAsync(client);
        }
    }
}