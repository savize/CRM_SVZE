using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BusEntity;

namespace DAL
{
    public class DB : DbContext
    {
        public DB() : base ("ConnStr") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityCategory> activityCategories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reminder> Reminders { get; set; }


    }
}
