using SalonApp.Modules.Services.Domain;
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

public partial class ServicesView : UserControl
{
    public ServicesView()
    {
        InitializeComponent();
    }

    private async void AddService_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ServicesViewModel vm)
            await vm.AddServiceAsync();
    }

    private async void DeleteService_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Service service && DataContext is ServicesViewModel vm)
            await vm.DeleteServiceAsync(service);
    }
}
