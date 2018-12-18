using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class ProductionImage
    {
        public Guid Id { get; set; }
        [Required]
        public byte[] Content { get; set; }

        public Guid ProdId { get; set; }

        public bool IsCover { get; set; }
    }
}
