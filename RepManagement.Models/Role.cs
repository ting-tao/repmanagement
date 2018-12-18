using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepManagement.Models
{
    public class Role
    {
       
        public Guid Id { get; set; }
        public int RoleType { get; set; }
        public string RoleName { get; set; }

    }
}
