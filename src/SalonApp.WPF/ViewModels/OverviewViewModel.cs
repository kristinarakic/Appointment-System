using System.Collections.ObjectModel;
using System.ComponentModel;
using SalonApp.Modules.Appointments.Application;
using SalonApp.Modules.Appointments.Domain;
using SalonApp.Modules.Clients.Application;
using SalonApp.Modules.Clients.Domain;
using SalonApp.Modules.Staff.Application;
using SalonApp.Modules.Staff.Domain;
using SalonApp.Modules.Services.Application;
using SalonApp.Modules.Services.Domain;

namespace SalonApp.WPF.ViewModels;

public class OverviewViewModel : INotifyPropertyChanged
{
    private readonly SchedulingService _schedulingService;
    private readonly ClientService _clientService;
    private readonly StaffService _staffService;
    private readonly ServiceManager _serviceManager;

    public ObservableCollection<AppointmentDisplay> Appointments { get; set; } = new();
    public ObservableCollection<Client> Clients { get; set; } = new();
    public ObservableCollection<StaffMember> StaffMembers { get; set; } = new();
    public ObservableCollection<Service> Services { get; set; } = new();

    private Client? _filterClient;
    public Client? FilterClient
    {
        get => _filterClient;
        set { _filterClient = value; OnPropertyChanged(nameof(FilterClient)); }
    }

    private StaffMember? _filterStaff;
    public StaffMember? FilterStaff
    {
        get => _filterStaff;
        set { _filterStaff = value; OnPropertyChanged(nameof(FilterStaff)); }
    }

    private DateTime? _filterDate = null;
    public DateTime? FilterDate
    {
        get => _filterDate;
        set { _filterDate = value; OnPropertyChanged(nameof(FilterDate)); }
    }

    private bool _filterByDate = true;
    public bool FilterByDate
    {
        get => _filterByDate;
        set { _filterByDate = value; OnPropertyChanged(nameof(FilterByDate)); }
    }

    public OverviewViewModel(
        SchedulingService schedulingService,
        ClientService clientService,
        StaffService staffService,
        ServiceManager serviceManager)
    {
        _schedulingService = schedulingService;
        _clientService = clientService;
        _staffService = staffService;
        _serviceManager = serviceManager;

        LoadDataAsync();
    }

    private async void LoadDataAsync()
    {
        var clients = await _clientService.GetAllClientsAsync();
        foreach (var c in clients) Clients.Add(c);

        var staff = await _staffService.GetAllStaffMembersAsync();
        foreach (var s in staff) StaffMembers.Add(s);

        var services = await _serviceManager.GetAllServicesAsync();
        foreach (var s in services) Services.Add(s);
    }

    public async Task SearchAsync()
    {
        try
        {
            Appointments.Clear();

            var all = (await _schedulingService.GetAllAppointmentsAsync()).ToList();

            if (FilterDate != null)
                all = all.Where(a => a.DateTime.Date == FilterDate.Value.Date).ToList();

            if (FilterStaff != null)
                all = all.Where(a => a.StaffMemberId == FilterStaff.Id).ToList();

            if (FilterClient != null)
                all = all.Where(a => a.ClientId == FilterClient.Id).ToList();

            foreach (var a in all.OrderBy(a => a.DateTime))
            {
                Appointments.Add(new AppointmentDisplay
                {
                    Id = a.Id,
                    ClientName = Clients.FirstOrDefault(c => c.Id == a.ClientId)?.FullName ?? "Nepoznat",
                    StaffName = StaffMembers.FirstOrDefault(s => s.Id == a.StaffMemberId)?.FullName ?? "Nepoznat",
                    ServiceName = Services.FirstOrDefault(s => s.Id == a.ServiceId)?.Name ?? "Nepoznata",
                    DateTime = a.DateTime,
                    DurationInMinutes = a.DurationInMinutes,
                    Status = a.Status.ToString(),
                    Original = a
                });
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Greška");
        }
    }

    public async Task ClearFiltersAsync()
    {
        FilterClient = null;
        FilterStaff = null;
        FilterDate = null;
        await SearchAsync();
    }

    public async Task RefreshDataAsync()
    {
        Clients.Clear();
        StaffMembers.Clear();
        Services.Clear();

        var clients = await _clientService.GetAllClientsAsync();
        foreach (var c in clients) Clients.Add(c);

        var staff = await _staffService.GetAllStaffMembersAsync();
        foreach (var s in staff) StaffMembers.Add(s);

        var services = await _serviceManager.GetAllServicesAsync();
        foreach (var s in services) Services.Add(s);

        await SearchAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}