using GuideInvestimentosAPI.Business.Notifications;

namespace GuideInvestimentosAPI.Business.Interfaces.Notifications;

public interface INotificator
{
    IList<Notification> ReturnNotifications();
    void AddNotification(Notification notification);
    bool EmptyNotifications();
}