using System;
namespace NotificationService.Model
{
    public class NotificationLogs
    {
        public int Id { get; set; }
        public int Notification_id { get; set; }
        public string Type { get; set; }
        public int From { get; set; }
        public int Target { get; set; }
        public string Email_destination { get; set; }
        public DateTime Read_at { get; set; }
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;

        public Notification Notification { get; set; }
    }
}
