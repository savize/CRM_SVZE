using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusEntity
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RemindDate { get; set; }
        public  bool Status { get; set;}

        public Reminder()
        {
            Status = false;
        }

        public User user { get; set; }
    }
}
