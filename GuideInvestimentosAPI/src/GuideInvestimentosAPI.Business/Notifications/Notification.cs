namespace GuideInvestimentosAPI.Business.Notifications;

public class Notification
{
    public string Error { get; }

    public Notification(string error)
    {
        Error = error;
    }

}