using BusEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DAL
{
    public class Reminderdal
    {
        DB db = new DB();

        public String Create(Reminder r, User u)
        {
            try
            {
                r.user = db.Users.Find(u.Id);
                db.Reminders.Add(r);
                db.SaveChanges();
                return "Reminder is added successfully";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public DataTable read()
        {
            string cmd = "SELECT dbo.Reminders.Title, dbo.Reminders.Description, dbo.Reminders.RemindDate AS [To Remind on], dbo.Reminders.Status, dbo.Users.Username AS [Related to], dbo.Reminders.RegDate AS [Creation Date], dbo.Reminders.Id, \r\n                  dbo.Reminders.user_Id, dbo.Users.Id AS Expr1\r\nFROM     dbo.Reminders INNER JOIN\r\n                  dbo.Users ON dbo.Reminders.user_Id = dbo.Users.Id";
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, conn);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public Reminder Read(int id)
        {
            return db.Reminders.Include("user").Where(i=> i.Id == id).FirstOrDefault();
        }

        public DataTable read(string s)
        {
            string cmd = "FROM     dbo.Reminders INNER JOIN\r\n                  dbo.Users ON dbo.Reminders.user_Id = dbo.Users.Id\r\nWHERE  (dbo.Reminders.Title like '%'+ @Search +'%') or (dbo.Users.Username like '%'+ @Search +'%') or (dbo.Reminders.Description like '%'+ @Search +'%')\r\n";
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
            return db.Reminders.Count();
        }

        public string Update(Reminder r, int id)
        {
            var q = db.Reminders.Where(i => i.Id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Title = r.Title;
                    q.Description = r.Description;
                    q.RemindDate = r.RemindDate;
                    q.user = r.user;

                    db.SaveChanges();
                    return "Reminder updated successfully";
                }

                else
                {
                    return "Reminder does not exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Delete(int id)
        {
            var q = db.Reminders.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    db.Reminders.Remove(q);
                    db.SaveChanges();
                    return "Reminder is removed";
                }
                else
                {
                    return "Reminder does not exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }          
        }


        public string OffStat(int id)
        {
            var q = db.Reminders.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null && q.Status == false)
                {
                    q.Status = true;
                    db.SaveChanges();
                    return "Reminder set as done!";
                }
                else if (q != null && q.Status == true)
                {
                    q.Status = false;
                    db.SaveChanges();
                    return "Reminder is on";
                }
                else
                {
                    return "Reminder not found";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
