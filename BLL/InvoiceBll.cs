using BusEntity;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    
    public class InvoiceBll
    {
        InvoiceDal invDal = new InvoiceDal();

        public string Create(Invoice i, Customer c, User u, List<Product> p)
        {
            return invDal.Create(i,c, u, p);
        }

        public string InvoiceNumber()
        {
            return invDal.InvoiceNumber();
        }

        public DataTable Read()
        {
            return invDal.Read();
        }

        public DataTable Read(string s)
        {
            return invDal.Read(s);
        }

        public int ReadCount()
        {
            return invDal.ReadCount();
        }

        public string SetPaid(int id)
        {
            return invDal.SetPaid(id);
        }

        public string Delete(int id)
        {
            return invDal.Delete(id);
        }

    }
}
