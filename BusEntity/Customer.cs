using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusEntity
{
    public class Customer
    {
        public int Id { get; set; }
        public string CusName { get; set; }
        public bool status { get; set; }

        public Customer()
        {
            status = true;
        }

        public string Phone { get; set; }
        public DateTime RegDate { get; set; }

        public List<Activity> Activities { get; set; } = new List<Activity>();

        public List<Invoice> invoices { get; set; } = new List<Invoice>();
    }
}
