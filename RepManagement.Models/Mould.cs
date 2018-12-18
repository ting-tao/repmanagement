using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class Mould
    {
     
        public Guid Id { get; set; }
        public string SerialNum { get; set; }
        public Guid TypeID { get; set; }
        public double? Size { get; set; }
        public string Spec { get; set; }
        public string Creator { get; set; }
        public string RelateMaterial { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }

        public Guid? FrontImgID { get; set; }
        public Guid? SideImgID { get; set; }
        public Guid? BackImgID { get; set; }
    }
}
