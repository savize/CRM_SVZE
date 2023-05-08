using BusEntity;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BLL
{
    public class productBll
    {
        ProductDal pdal = new ProductDal();
        public string Create(Product p)
        {  
            if (pdal.ReadIsExist(p))
            {
                return pdal.Create(p);
            }
            else
            {
                return "product already exist";
            }
        }

        public DataTable Read()
        {
            return pdal.Read();
        }

        public Product Read(int id)
        {
            return pdal.Read(id);
        }

        public DataTable Read(string s)
        {
            return pdal.Read(s);
        }

        public List<string> ReadList()
        {
            return pdal.ReadList();
        }

        public Product ReadProd(string s)
        {
            return pdal.ReadProd(s);
        }

        public int readCount()
        {
            return pdal.readCount();
        }

        public string Update(Product p, int id) 
        {
            return pdal.Update(p, id);
        }

        public string Delete(int id)
        {
            return pdal.Delete(id);
        }
    
    }
}
