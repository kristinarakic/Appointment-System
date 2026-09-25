<<<<<<< HEAD
﻿using SalonApp.SharedKernel;
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
=======
﻿using SalonApp.SharedKernel;
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
>>>>>>> 61b15be885371087dfd56086c3649952e5f1ef39
}