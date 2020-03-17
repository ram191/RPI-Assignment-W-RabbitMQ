using System;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.Model
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {
        }

        public DbSet<Notification>Notifications { get; set; }
        public DbSet<NotificationLogs>Notification_logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder
                .Entity<NotificationLogs>()
                .HasOne(x => x.Notification)
                .WithMany()
                .HasForeignKey(x => x.Notification_id);
        }

    }
}
