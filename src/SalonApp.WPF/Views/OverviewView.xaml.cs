using System.Windows;
using System.Windows.Controls;
using SalonApp.WPF.ViewModels;

namespace SalonApp.WPF.Views;

public partial class OverviewView : UserControl
{
    public OverviewView()
    {
        InitializeComponent();
    }

    private async void Search_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OverviewViewModel vm)
            await vm.SearchAsync();
    }

    private async void ClearFilters_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OverviewViewModel vm)
            await vm.ClearFiltersAsync();
    }
}