using BusEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;

namespace DAL
{
    public class ActivityDal
    {
        DB db = new DB();

        public String Create(Activity a, User u, Customer c, ActivityCategory ac)
        {
            try
            {
                a.User = db.Users.Find(u.Id);
                a.Customer = db.Customers.Find(c.Id);
                a.Category = db.activityCategories.Find(ac.Id);


                db.Activities.Add(a);
                db.SaveChanges();
                return "Activity is added successfully";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public DataTable read()
        {
            string cmd = "SELECT dbo.Activities.Id, dbo.ActivityCategories.CatName AS Category, dbo.Activities.Title, dbo.Activities.Description, dbo.Users.Username AS [User], dbo.Customers.CusName AS Customer, dbo.Activities.date AS Date\r\nFROM     dbo.Activities INNER JOIN\r\n                  dbo.ActivityCategories ON dbo.Activities.Category_Id = dbo.ActivityCategories.Id INNER JOIN\r\n                  dbo.Customers ON dbo.Activities.Customer_Id = dbo.Customers.Id INNER JOIN\r\n                  dbo.Users ON dbo.Activities.User_Id = dbo.Users.Id";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public Activity Read(int id)
        {
            return db.Activities.Include("Category").Where(i=> i.Id == id).FirstOrDefault();
        }

        public DataTable Read(string s)
        {
            string cmd = "SELECT dbo.Activities.Id, dbo.ActivityCategories.CatName AS Category, dbo.Activities.Title, dbo.Activities.Description, dbo.Users.Username AS [User], dbo.Customers.CusName AS Customer, dbo.Activities.date AS Date\r\nFROM     dbo.Activities INNER JOIN\r\n                  dbo.ActivityCategories ON dbo.Activities.Category_Id = dbo.ActivityCategories.Id INNER JOIN\r\n                  dbo.Customers ON dbo.Activities.Customer_Id = dbo.Customers.Id INNER JOIN\r\n                  dbo.Users ON dbo.Activities.User_Id = dbo.Users.Id\r\nWHERE  (dbo.Customers.CusName like '%' + @Search + '%') or (dbo.Users.Username like '%' +@Search + '%') or (dbo.Activities.Title like '%' +@Search + '%') or (dbo.ActivityCategories.CatName like '%' +@Search + '%')";
            SqlCommand sqcomn = new SqlCommand(cmd);
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");

            sqcomn.Connection = conn;
            sqcomn.CommandType = CommandType.Text;
            sqcomn.Parameters.AddWithValue("@Search", s);

            var sqladapter = new SqlDataAdapter(cmd, conn);
            sqladapter.SelectCommand = sqcomn;
            var commandbuilder = new SqlCommandBuilder(sqladapter);

            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public int ReadCount()
        {
            return db.Activities.Count();
        }

        public string Update(Activity a, int id)
        {
            var q = db.Activities.Where(i => i.Id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Title = a.Title;
                    q.Description = a.Description;
                    q.date = a.date;
                    q.Category = a.Category;
                    

                    db.SaveChanges();
                    return "Activity updated successfully";
                }

                else
                {
                    return "Activity does not exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Delete(int id)
        {
            var q = db.Activities.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    db.Activities.Remove(q);
                    db.SaveChanges();
                    return "Activity is removed";
                }
                else
                {
                    return "Activity does not exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
