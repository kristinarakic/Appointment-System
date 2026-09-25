using System.Collections.ObjectModel;
using System.ComponentModel;
using SalonApp.Modules.Notifications.Application;
using SalonApp.Modules.Notifications.Domain;

namespace SalonApp.WPF.ViewModels;

public class NotificationsViewModel : INotifyPropertyChanged
{
    private readonly NotificationService _notificationService;

    public ObservableCollection<Notification> Notifications { get; set; } = new();

    public NotificationsViewModel(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task LoadNotificationsAsync()
    {
        var notifications = await _notificationService.GetAllNotificationsAsync();
        Notifications.Clear();
        foreach (var n in notifications.OrderByDescending(n => n.CreatedAt))
        {
            Notifications.Add(n);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}