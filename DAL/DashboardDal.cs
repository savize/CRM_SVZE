using BusEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DashboardDal
    {
        DB db = new DB();

        public string CusCount()
        {
            return db.Customers.Count().ToString();
        }
        public string CusActCount()
        {
            return db.Customers.Where(i=> i.status == true).Count().ToString();
        }

        public string RemCount(User u)
        {
            User q = db.Users.Include("Reminders").Where(i=> i.Id == u.Id).FirstOrDefault();
            return q.Reminders.Count().ToString();
        }

        public string RemCountToday(User u)
        {
            int sum = 0;
            User source = db.Users.Include("Reminders").Where(r => r.Id == u.Id).FirstOrDefault();
            foreach (var item in source.Reminders)
            {
                if(item.RemindDate == DateTime.Today)
                {
                    sum++;
                }
            }
            return sum.ToString();
        }

        public List<Reminder> UserReminders(User u)
        {
            return db.Reminders.Include("user").Where(o=> o.user.Id == u.Id && o.RemindDate == DateTime.Today && o.Status == false).ToList();
        }

        public string InvCount(User u) 
        {
            User q = db.Users.Include("invoices").Where(x=> x.Id == u.Id).FirstOrDefault();
            return q.invoices.Count().ToString();
        }

        public Reminder readRem(string title)
        {
            return db.Reminders.Where(l => l.Title == title).FirstOrDefault();
        }
    }
}
