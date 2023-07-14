using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core.Filters;

namespace UserMgt.Core.Entities.Dtos
{
    /// <summary>
    /// Represents User Object
    /// </summary>
    public record UserDTO
    {
        /// <summary>
        /// The Users Email Address
        /// </summary>
        [Required]
        [EmailAddress]
        [ValidateDuplicateUser]
        public string? EmailAddress { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public record UpdateUserDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        [ValidateDuplicateUser]
        public string? EmailAddress { get; set; }

    }

}
