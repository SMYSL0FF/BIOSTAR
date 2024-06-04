using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIOSTAR.ViewModels
{
    public class OrderViewModel
    {
        public int order_id { get; set; }
        public string description { get; set; }
        public DateTime appointment_date { get; set; }
        public string status { get; set; }
        public static List<string> StatusOptions { get; } = new List<string> { "Pending", "In Progress", "Completed", "Cancelled" };
    }
}


