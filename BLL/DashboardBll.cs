using BusEntity;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DashboardBll
    {
        DashboardDal dashdl = new DashboardDal();

        public string CusCount()
        {
            return dashdl.CusCount();
        }

        public string CusActCount()
        {
            return dashdl.CusActCount();
        }

        public string RemCount(User u)
        {
            return dashdl.RemCount(u);
        }

        public List<Reminder> UserReminders(User u)
        {
            return dashdl.UserReminders(u);
        }

        public string InvCount(User u)
        {
            return dashdl.InvCount(u);
        }

        public Reminder readRem(string title)
        {
            return dashdl.readRem(title);
        }

    }
}
