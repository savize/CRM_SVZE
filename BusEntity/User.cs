using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusEntity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Pic { get; set; }
        public bool Status { get; set; }

        public User()
        {
            Status = true;
        }

        public DateTime RegDate { get; set; }

        public List<Activity> Activities { get; set; } = new List<Activity>();

        public List<Invoice> invoices { get; set; } = new List<Invoice>();

        public List<Reminder> Reminders { get; set; } = new List<Reminder>();
    }
}
