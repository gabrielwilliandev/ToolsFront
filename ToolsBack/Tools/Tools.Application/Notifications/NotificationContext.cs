namespace Tools.Application.Notifications
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications = new();
        public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
        public bool HasNotifications => _notifications.Any();
        public void AddNotification(string property, string message)
        {
            _notifications.Add(new Notification(property, message));
        }
        public void ClearNotifications()
        {
            _notifications.Clear();
        }
    }
}
