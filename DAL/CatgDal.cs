using BusEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CatgDal
    {
        DB db = new DB();

        public string Create(ActivityCategory ac)
        {
            db.activityCategories.Add(ac);
            db.SaveChanges();
            return "Category added successfully";
        }

        public DataTable Read()
        {
            string cmd = "SELECT Id, CatName AS [Category Name]\r\nFROM     dbo.ActivityCategories";
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, conn);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public ActivityCategory Read(int id)
        {
            return db.activityCategories.Where(u=> u.Id == id).FirstOrDefault();
        }

        public List<string> ReadListCategories()
        {
            return db.activityCategories.Select(i=> i.CatName).ToList();
        }

        public ActivityCategory ReadCategory(string s)
        {
            return db.activityCategories.Where(i=> i.CatName == s).FirstOrDefault();
        }

        public int ReadCount()
        {
            return db.activityCategories.Count();
        }

        public string Update(int id, ActivityCategory ac)
        {
            var q = db.activityCategories.Where(c => c.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.CatName = ac.CatName;
                    db.SaveChanges();
                    return "Category Updated";
                }
                else
                {
                    return "Category not found";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.activityCategories.Where(o=> o.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    db.activityCategories.Remove(q);
                    db.SaveChanges();
                    return "Selected Category is removed";
                }
                else
                {
                    return "Category not found";
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }

        }
    }
}
