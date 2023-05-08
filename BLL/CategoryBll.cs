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
    public class CategoryBll
    {
        CatgDal catdl = new CatgDal();

        public string Create(ActivityCategory ac)
        {
            return catdl.Create(ac);
        }

        public DataTable Read()
        {
            return catdl.Read();
        }

        public ActivityCategory Read(int id)
        {
            return catdl.Read(id);
        }

        public List<string> ReadListCategories()
        {
            return catdl.ReadListCategories();
        }

        public ActivityCategory ReadCategory(string s)
        {
            return catdl.ReadCategory(s);
        }

        public int ReadCount()
        {
            return catdl.ReadCount();
        }

        public string Update(int id, ActivityCategory ac)
        {
            return catdl.Update(id, ac);
        }

        public string Delete(int id)
        {
            return catdl.Delete(id);
        }

    }
}
