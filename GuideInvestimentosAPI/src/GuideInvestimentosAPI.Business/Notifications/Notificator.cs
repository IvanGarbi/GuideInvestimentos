using GuideInvestimentosAPI.Business.Interfaces.Notifications;

namespace GuideInvestimentosAPI.Business.Notifications;

public class Notificator : INotificator
{
    private readonly IList<Notification> _notifications;

    public Notificator()
    {
        _notifications = new List<Notification>();
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public bool EmptyNotifications()
    {
        return _notifications.Any();
    }

    public IList<Notification> ReturnNotifications()
    {
        return _notifications;
    }
}