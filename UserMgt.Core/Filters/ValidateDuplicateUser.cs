using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.Core.Filters
{
    public class ValidateDuplicateUser : ValidationAttribute
    {
        public string GetErrorMessage() =>
                $"User with the email address has already registered";
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            UserDbContext? _context = validationContext
                            .GetService(typeof(UserDbContext)) as UserDbContext;

            if (value != null)
                if (_context!.Users.Any(r => r.EmailAddress == value.ToString()))
                {                  
                    return new ValidationResult(GetErrorMessage());
                }

            return ValidationResult.Success!;
        }
    }
}
