using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusEntity
{
    public class ActivityCategory
    {
        public int Id { get; set; }
        public string CatName { get; set; }

        public List<Activity> Activities { get; set; } = new List<Activity>();
        
    }
}
