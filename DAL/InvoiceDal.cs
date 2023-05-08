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
    public class InvoiceDal
    {
        DB db = new DB();
        public string Create(Invoice i, Customer c, User u, List<Product> p)
        {
            i.Customer = db.Customers.Find(c.Id);
            i.User = db.Users.Find(u.Id);

            foreach (var item in p)
            {
                i.products.Add(db.Products.Find(item.Id));
            }

            Random rnd = new Random();
            string s = rnd.Next(1000000).ToString();
            var q = db.Invoices.Where(j => j.InvNumber == s);
            
            while(q.Count() > 0)
            {
                s = rnd.Next(1000000).ToString(); 
            }
            i.InvNumber = s;
            
            db.Invoices.Add(i);
            db.SaveChanges();

            return "Invoice created successfully";
        }

        public DataTable Read()
        {
            string cmd = "SELECT dbo.Invoices.Id , dbo.Invoices.InvNumber AS [Invoice Number], dbo.Invoices.RegDate AS Issue, dbo.Customers.CusName AS Customer, dbo.Users.Username AS Seller, dbo.Invoices.TotalPrice AS [Total Price], dbo.Invoices.Status, dbo.Invoices.ChckoutDate AS [Check Out], dbo.Users.Name\r\nFROM     dbo.Invoices INNER JOIN\r\n                  dbo.Customers ON dbo.Invoices.Customer_Id = dbo.Customers.Id INNER JOIN\r\n                  dbo.Users ON dbo.Invoices.User_Id = dbo.Users.Id";
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=MomoCRM;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, conn);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        
        public DataTable Read(string s)
        {
            string cmd = "SELECT dbo.Invoices.Id , dbo.Invoices.InvNumber AS [Invoice Number], dbo.Invoices.RegDate AS Issue, dbo.Customers.CusName AS Customer, dbo.Users.Username AS Seller, dbo.Invoices.TotalPrice AS [Total Price], dbo.Invoices.Status, dbo.Invoices.ChckoutDate AS [Check Out], dbo.Users.Name\r\nFROM     dbo.Invoices INNER JOIN\r\n                  dbo.Customers ON dbo.Invoices.Customer_Id = dbo.Customers.Id INNER JOIN\r\n                  dbo.Users ON dbo.Invoices.User_Id = dbo.Users.Id WHERE ((CusName like '%'+ @search + '%') or (Username like '%'+ @search + '%') or (InvNumber like '%'+ @search + '%'))\r\n";
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

        public int ReadCount()
        {
            return db.Invoices.Count();
        }

        public string SetPaid(int id)
        {
            var q = db.Invoices.Where(i => i.Id == id).FirstOrDefault();
            q.Status = true;
            q.ChckoutDate = DateTime.Now;
            db.SaveChanges();
            return "Selected invoice set as paid";
        }

        public string InvoiceNumber()
        {
            var q = db.Invoices.OrderByDescending(i=> i.Id).FirstOrDefault();
            return q.InvNumber;
        }

        public string Delete(int id)
        {
            var q = db.Invoices.Where(i => i.Id == id).FirstOrDefault();
            db.Invoices.Remove(q);
            db.SaveChanges();
            return "Invoice deleted successfully";
        }
    }
}
