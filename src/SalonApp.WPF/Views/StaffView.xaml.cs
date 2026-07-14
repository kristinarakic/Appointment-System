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
using SalonApp.Modules.Staff.Domain;
using SalonApp.WPF.ViewModels;

namespace SalonApp.WPF.Views
{
    /// <summary>
    /// Interaction logic for StaffView.xaml
    /// </summary>
    public partial class StaffView : UserControl
    {
        public StaffView()
        {
            InitializeComponent();
        }
        private async void AddStaff_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StaffViewModel vm)
                await vm.AddStaffMemberAsync();
        }

        private async void UpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StaffViewModel vm)
                await vm.UpdateStaffMemberAsync();
        }

        private async void DeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is StaffMember member && DataContext is StaffViewModel vm)
                await vm.DeleteStaffMemberAsync(member);
        }
    }
}
