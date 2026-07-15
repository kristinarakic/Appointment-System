using SalonApp.Modules.Appointments.Domain;

namespace SalonApp.WPF.ViewModels;

public class AppointmentDisplay
{
    public int Id { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string StaffName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public int DurationInMinutes { get; set; }
    public string Status { get; set; } = string.Empty;
    public Appointment Original { get; set; } = null!;
}