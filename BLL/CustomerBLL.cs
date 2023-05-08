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
    public class CustomerBLL
    {
        CustomerDAL cusdal = new CustomerDAL();
        
        public string Create(Customer c)
        {
            if (cusdal.ReadifExist(c))
            {
                return cusdal.Create(c);
            }
            else
            {
                return "Customer is registered before";
            }
        }

        public DataTable Read()
        {
            return cusdal.Read();
        }

        public DataTable Read(string s)
        {
           return cusdal.Read(s);
        }

        public DataTable ReadAct()
        { 
            return cusdal.ReadAct();
        }

            public Customer ReadCus(string s)
        {
            return cusdal.ReadCus(s);
        }

        public List<string> ReadCuslist()
        {
            return cusdal.ReadCuslist();
        }


        public Customer Read(int id)
        {
            return cusdal.Read(id);
        }

        public int ReadCusAll()
        {
            return cusdal.ReadCusAll();
        }

        public int ReadCusAct()
        {
            return cusdal.ReadCusAct();
        }

        public string Update(int id, Customer c) 
        {
            return cusdal.Update(id, c);       
        }

        public string Delete(int id)
        {
            return cusdal.Delete(id);
        }

        public int ReadCusReport(int month)
        {
            return cusdal.ReadCusReport(month);
        }

    }
}
