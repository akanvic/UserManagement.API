using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.Core.Entities.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
