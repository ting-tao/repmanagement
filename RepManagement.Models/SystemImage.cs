using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class SystemImage
    {
       
        public Guid Id { get; set; }
        [Required]
        public byte[] Content { get; set; }
    }
}
