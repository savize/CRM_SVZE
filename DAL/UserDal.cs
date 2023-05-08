using BusEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDal
    {
        DB db = new DB();

        public string Create(User u)
        {
            try
            {
                db.Users.Add(u);
                db.SaveChanges();
                return "User registered successfully";
            }
            catch (Exception e)
            {
                return "User registration failed: " + e.Message;
            }
        }

        public bool ReadNotExist(User u)
        {
            var q = db.Users.Where(i => i.Username == u.Username);

            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable Read()
        {
            string cmd = "SELECT Id, Name, Username, Status, RegDate AS [Registration Date], Pic\r\nFROM     dbo.Users";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable ReadActive()
        {
            string cmd = "SELECT Id, Name, Username, Status, Pic, RegDate AS [Registeration Date]\r\nFROM     dbo.Users\r\nWHERE  (Status = 1)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable Read(string s)
        {
            string cmd = "SELECT Id, Name, Username, Status, RegDate AS [Registration Date]\r\nFROM     dbo.Users WHERE ((Name like '%'+ @search + '%') or (Username like '%'+ @search + '%'))";
            SqlCommand sqcomn = new SqlCommand(cmd);
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");

            sqcomn.Connection = conn;
            sqcomn.CommandType = CommandType.Text;
            sqcomn.Parameters.AddWithValue("@search", s);

            var sqladapter = new SqlDataAdapter(cmd, conn);
            sqladapter.SelectCommand = sqcomn;
            var commandbuilder = new SqlCommandBuilder(sqladapter);

            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public User Read(int id)
        {
            return db.Users.Where(i => i.Id == id).FirstOrDefault();
        }

        public User ReadUser(string s)
        {
            return db.Users.Where(i=> i.Username == s).FirstOrDefault();
        }

        
        public List<string> ReadUsernames()
        {
            return db.Users.Where(i => i.Status == true).Select(i=> i.Username).ToList();
        }

        public List<User> ReadUsers()
        {
            return db.Users.Include("invoices").Where(o => o.Status == true).ToList();
        }

        public List<User> ReadUsersActiv()
        {
            return db.Users.Include("Activities").Where(u => u.Status == true).ToList();
        }

        public int ReadCountAll()
        {
            return db.Users.Count();
        }
        public int ReadCountAct()
        {
            return db.Users.Where(u=> u.Status == true).Count();
        }

        public string Update(User U, int id)
        {
            var q = db.Users.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.Username = U.Username;
                    q.Password = U.Password;
                    q.Name = U.Name;
                    q.Pic = U.Pic;

                    db.SaveChanges();
                    return "User updated successfully";
                }
                else
                {
                    return "User does not exist";
                }

            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public string Delete(int id)
        {
            var q = db.Users.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.Status = false;
                    db.SaveChanges();
                    return "User Status set to Non-Active";
                }
                else
                {
                    return "User is not found";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public User Login(string user, string pass)
        {
            return db.Users.Where(i=> i.Username == user && i.Password == pass).FirstOrDefault();
        }

        public string SetStat(int id)
        {
            var q = db.Users.Where(u=> u.Id == id).FirstOrDefault();

            try
            {
                if (q!=null && q.Status == false)
                {
                    q.Status = true;
                    db.SaveChanges();
                    return "User state changed to Active";
                }
                else if (q != null && q.Status == true)
                {
                    q.Status = false;
                    db.SaveChanges();
                    return "User state changed to Non-Active";
                }
                else
                {
                    return "User not found";
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
            
            
        }

    }
}
