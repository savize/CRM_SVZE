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
    public class ProductDal
    {
        DB db = new DB();
        public string Create(Product p)
        {
            try
            {
                db.Products.Add(p);
                db.SaveChanges();
                return "Product added successfully";
            }
            catch (Exception e)
            {
                return "Product is not added: " + e.Message;
            }       
        }

        public bool ReadIsExist(Product P)
        {
            var q = db.Products.Where(i => i.Name == P.Name);

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
            string cmd = "SELECT Id, Name AS [Item Name], Price AS [Item Price], Stock AS [Item Quantity]\r\nFROM     dbo.Products";
            SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=MomoCRM;Trusted_Connection=True;;");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable Read(string s)
        {
            string cmd = "SELECT Id, Name AS [Item Name], Price AS [Item Price], Stock AS [Item Quantity]\r\nFROM     dbo.Products WHERE Name like '%'+ @search + '%'";
            SqlCommand sqcomn = new SqlCommand(cmd);
            SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=MomoCRM;Trusted_Connection=True;");

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

        public Product Read(int id)
        {
            return db.Products.Where(i => i.Id == id).FirstOrDefault();
        }

        public List<string> ReadList()
        {
            return db.Products.Select(i=> i.Name).ToList();
        }

        public Product ReadProd(string s)
        {
            return db.Products.Where(i=> i.Name == s).FirstOrDefault();
        }

        public int readCount()
        {
            return db.Products.Count();
        }

        public String Update(Product p, int id)
        {
            var q = db.Products.Where(i => i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    q.Name = p.Name;
                    q.Price = p.Price;
                    q.Stock = p.Stock;

                    db.SaveChanges();
                    return "Product updated successfully";
                }

                else
                {
                    return "Item does not exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Delete(int id)
        {
            var q = db.Products.Where(i=> i.Id == id).FirstOrDefault();

            try
            {
                if (q != null)
                {
                    db.Products.Remove(q);
                    db.SaveChanges();
                    return "Item is removed";
                }
                else
                {
                    return "Item does not exist";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
