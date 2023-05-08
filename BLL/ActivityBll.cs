using BusEntity;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ActivityBll
    {
        ActivityDal acd = new ActivityDal();
        public String Create(Activity a, User u, Customer c, ActivityCategory ac)
        {
            return acd.Create(a, u, c, ac);
        }

        public DataTable read()
        {
            return acd.read();
        }

        public Activity Read(int id)
        {
            return acd.Read(id);
        }

        public DataTable Read(string s)
        {
            return acd.Read(s);         
        }

        public int ReadCount()
        {
            return acd.ReadCount();
        }

        public string Update(Activity a, int id)
        {
          return acd.Update(a, id);
        }

        public string Delete(int id)
        {
            return acd.Delete(id);
        }
    }
}
