using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class Vendor
    {
        
        public Guid Id { get; set; }
        public string SerialNum { get; set; }
        public string VendorName { get; set; }
        public Guid TypeID { get; set; } 
        public string Style { get; set; }
        public string Level { get; set; }
        public double? Production { get; set; }
        public double? Deadline { get; set; }
        public string CustomerEval { get; set; }
        public string FactoryEval { get; set; }
        public string ContactPhone { get; set; }
        public string ManagerPhone { get; set; }
        public string ManagerName { get; set; }
        public string Address { get; set; }
        public double? Years { get; set; }
    }
}
