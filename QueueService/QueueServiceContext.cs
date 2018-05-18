namespace QueueService
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class QueueServiceContext : DbContext
    {
        public QueueServiceContext()
            : base("name=QueueServiceContext")
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}