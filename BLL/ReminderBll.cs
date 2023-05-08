using BusEntity;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReminderBll
    {
        Reminderdal remdal = new Reminderdal();

        public String Create(Reminder r, User u)
        {
            return remdal.Create(r, u);
        }

        public DataTable Read()
        {
            return remdal.read();
        }

        public Reminder Read(int id)
        {
            return remdal.Read(id);
        }

        public DataTable Read(string s)
        {
            return remdal.read(s);
        }

        public int ReadCount()
        {
            return remdal.ReadCount();
        }

        public string Update(Reminder r, int id)
        {
            return remdal.Update(r, id);
        }

        public string Delete(int id)
        {
            return remdal.Delete(id);
        }

        public String OffStat(int id)
        {
            return remdal.OffStat(id);
        }
    }
}
