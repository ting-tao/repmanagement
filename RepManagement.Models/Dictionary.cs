using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class Dictionary
    {
        
        public Guid Id { get; set; }
        [Required]
        public DictType DicTypeID { get; set; }
        [Required]
        public string TypeName { get; set; }
        public string Description { get; set; }
        

    }
}
