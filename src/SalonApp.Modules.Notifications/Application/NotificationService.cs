using SalonApp.SharedKernel;
using SalonApp.Modules.Notifications.Domain;

namespace SalonApp.Modules.Notifications.Application;

public class NotificationService
{
    private readonly IRepository<Notification> _repository;

    public NotificationService(IRepository<Notification> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
    {
        return await _repository.GetAllAsync();
    }
}