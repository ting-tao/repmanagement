using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class UserRole
    {
       
        public Guid Id { get; set; }

        public Guid UserID { get; set; }
      

        public Guid RoleID { get; set; }
        
    }
}
