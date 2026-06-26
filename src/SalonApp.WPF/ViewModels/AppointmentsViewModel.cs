using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonApp.Modules.Appointments.Application;
using SalonApp.Modules.Appointments.Domain;
using SalonApp.Modules.Clients.Application;
using SalonApp.Modules.Clients.Domain;
using SalonApp.Modules.Services.Application;
using SalonApp.Modules.Services.Domain;
using SalonApp.Modules.Staff.Application;
using SalonApp.Modules.Staff.Domain;

namespace SalonApp.WPF.ViewModels
{
    public class AppointmentsViewModel : INotifyPropertyChanged
    {
        private readonly SchedulingService _schedulingService;
        private readonly ClientService _clientService;
        private readonly ServiceManager _serviceManager;
        private readonly StaffService _staffService;

        public ObservableCollection<Appointment> Appointments { get; set; } = new();
        public ObservableCollection<Client> Clients { get; set; } = new();
        public ObservableCollection<Service> Services { get; set; } = new();
        public ObservableCollection<StaffMember> StaffMembers { get; set; } = new();
        public ObservableCollection<TimeSpan> AvailableSlots { get; set; } = new();

        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }

        private Service? _selectedService;
        public Service? SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
                if (value != null) LoadAvailableSlotsAsync();
            }
        }

        private StaffMember? _selectedStaff;
        public StaffMember? SelectedStaff
        {
            get => _selectedStaff;
            set
            {
                _selectedStaff = value;
                OnPropertyChanged(nameof(SelectedStaff));
                if (value != null) LoadAvailableSlotsAsync();
            }
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                LoadAvailableSlotsAsync();
            }
        }

        private TimeSpan? _selectedSlot;
        public TimeSpan? SelectedSlot
        {
            get => _selectedSlot;
            set { _selectedSlot = value; OnPropertyChanged(nameof(SelectedSlot)); }
        }

        public AppointmentsViewModel(
            SchedulingService schedulingService,
            ClientService clientService,
            ServiceManager serviceManager,
            StaffService staffService)
        {
            _schedulingService = schedulingService;
            _clientService = clientService;
            _serviceManager = serviceManager;
            _staffService = staffService;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            var clients = await _clientService.GetAllClientsAsync();
            foreach (var c in clients) Clients.Add(c);

            var services = await _serviceManager.GetAllServicesAsync();
            foreach (var s in services) Services.Add(s);

            var staff = await _staffService.GetAllStaffMembersAsync();
            foreach (var s in staff) StaffMembers.Add(s);
        }

        private async void LoadAvailableSlotsAsync()
        {
            if (SelectedStaff == null || SelectedService == null) return;

            var slots = await _schedulingService.FindAvailableSlotsAsync(
                SelectedStaff.Id,
                SelectedDate,
                SelectedService.DurationInMinutes,
                new TimeSpan(9, 0, 0),
                new TimeSpan(17, 0, 0));

            AvailableSlots.Clear();
            foreach (var slot in slots)
            {
                AvailableSlots.Add(slot);
            }
        }

        public async Task CreateAppointmentAsync()
        {
            if (SelectedClient == null || SelectedStaff == null ||
                SelectedService == null || SelectedSlot == null) return;

            var dateTime = SelectedDate.Date + SelectedSlot.Value;

            await _schedulingService.CreateAppointmentAsync(
                SelectedClient.Id,
                SelectedStaff.Id,
                dateTime,
                SelectedService.Id,
                SelectedService.DurationInMinutes);

            LoadAvailableSlotsAsync();
            await LoadAppointmentsAsync();
        }

        public async Task LoadAppointmentsAsync()
        {
            if (SelectedStaff == null) return;

            var appointments = await _schedulingService
                .GetAppointmentsByStaffAndDateAsync(SelectedStaff.Id, SelectedDate);

            Appointments.Clear();
            foreach (var a in appointments)
            {
                Appointments.Add(a);
            }
        }

        public async Task CancelAppointmentAsync(Appointment appointment)
        {
            await _schedulingService.CancelAppointmentAsync(appointment.Id);
            await LoadAppointmentsAsync();
            LoadAvailableSlotsAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
