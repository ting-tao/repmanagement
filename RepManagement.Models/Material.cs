using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class Material
    {
       
        public Guid Id { get; set; }
        [Required]
        public string SerialNum { get; set; }
        public Guid TypeID { get; set; }
        public Guid VendorID { get; set; }
        public string MaterialQuality { get; set; }
        public string Spec { get; set; }
        public string OpenSize { get; set; }
        public double? Weight { get; set; }
        public string ColorNum { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public double? LeftCount { get; set; }
        public Guid? IconID { get; set; }


    }
}
