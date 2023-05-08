using BusEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Data.Entity;

namespace DAL
{
    public class CustomerDAL
    {
        DB db = new DB();

        public string Create(Customer c)
        {
            try
            {
                db.Customers.Add(c);
                db.SaveChanges();
                return "Customer registration is successful";
            }
            catch (Exception e)
            {
                return "Customer registration failed: \n" + e.Message;         
            }           
        }

        public bool ReadifExist(Customer c)
        {
            var q = db.Customers.Where(i => i.Phone == c.Phone);

            if (q.Count() == 0)
            {
                return true;
            }
            else { return false; }
        }
        public DataTable Read()
        {
            string cmd = "SELECT Id, CusName AS [Customer Name], Phone, RegDate AS [Registration Date], status\r\nFROM     dbo.Customers";
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, conn);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];     
        }

        public DataTable ReadAct()
        {
            string cmd = "SELECT Id, CusName AS [Customer Name], Phone, RegDate AS [Registration Date], status\r\nFROM     dbo.Customers\r\nWHERE  (status = 1)";
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, conn);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable Read(string s)
        {
            string cmd = "SELECT Id, CusName AS [Customer Name], Phone, RegDate AS [Registration Date], status\r\nFROM     dbo.Customers WHERE ((Phone like '%'+ @search + '%') or (CusName like '%'+ @search + '%'))";
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

        public Customer Read(int id)
        {
            return db.Customers.Where(i=> i.Id == id).FirstOrDefault();
        }

        public Customer ReadCus(string s)
        {
            return db.Customers.Where(i=> i.CusName == s).FirstOrDefault();
        }
        
        public List<string> ReadCuslist()
        {
            return db.Customers.Where(i=> i.status == true).Select(i=> i.CusName).ToList();
        }
        
        public int ReadCusReport(int month)
        {
            return db.Customers.Where(x=> x.status == true && x.RegDate.Month == month).Count();
        }
        public int ReadCusAll()
        {
            return db.Customers.Count();
        }

        public int ReadCusAct()
        {
            return db.Customers.Where(c=> c.status == true).Count();
        }

        public string Update(int id , Customer c)
        {
            var q = db.Customers.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.CusName = c.CusName;
                    q.Phone = c.Phone;
                    db.SaveChanges();
                    return "Customer's info is updated";
                }
                else
                {
                    return "Customer is not exist";
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
        
        }

        public string Delete(int id) 
        {
            var q = db.Customers.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.status = false;
                    db.SaveChanges();
                    return "Done!";
                }
                else
                {
                    return "customer is not exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
