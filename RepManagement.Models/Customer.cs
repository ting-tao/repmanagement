using System;
using System.Collections.Generic;
using System.Text;

namespace RepManagement.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string SerialNum { get; set; }
        public string CustomerName { get; set; }
        public Guid TypeID { get; set; }
        public string Style { get; set; }
        public string Level { get; set; }
        public string Demand{ get; set; }
        public double? Amount { get; set; }
        public string CustomerEval { get; set; }
        public string FactoryEval { get; set; }
        public string ContactPhone { get; set; }
        public string ManagerPhone { get; set; }
        public string ManagerName { get; set; }
        public string Address { get; set; }
        public double? Years { get; set; }

    }
}
