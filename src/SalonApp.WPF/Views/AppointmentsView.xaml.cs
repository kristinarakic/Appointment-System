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
using SalonApp.Modules.Appointments.Domain;
using SalonApp.WPF.ViewModels;

namespace SalonApp.WPF.Views
{

    public partial class AppointmentsView : UserControl
    {
        public AppointmentsView()
        {
            InitializeComponent();
        }

        private async void CreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AppointmentsViewModel vm)
                await vm.CreateAppointmentAsync();
        }

        private async void CancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Appointment appointment && DataContext is AppointmentsViewModel vm)
                await vm.CancelAppointmentAsync(appointment);
        }
    }
}
