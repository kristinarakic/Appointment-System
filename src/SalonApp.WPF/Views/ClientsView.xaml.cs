using SalonApp.Modules.Clients.Domain;
using SalonApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalonApp.WPF.Views;

public partial class ClientsView : UserControl
{
    public ClientsView()
    {
        InitializeComponent();
    }

    private async void AddClient_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ClientsViewModel vm)
            await vm.AddClientAsync();
    }

    private async void DeleteClient_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Client client && DataContext is ClientsViewModel vm)
            await vm.DeleteClientAsync(client);
    }
}
