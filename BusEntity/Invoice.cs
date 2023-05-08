using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusEntity
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvNumber { get; set; }
        public DateTime RegDate { get; set; }
        public Nullable<DateTime> ChckoutDate { get; set; }
        public bool Status { get; set; }
        public double TotalPrice { get; set; }
        public Customer Customer { get; set; }
        public User User { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
    
    }
}
