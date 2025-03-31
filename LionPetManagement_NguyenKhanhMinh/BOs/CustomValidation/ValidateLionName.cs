using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessObjectLayer.CustomValidation
{
    public class ValidateLionName : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Lion Name is required.");
            }

            string lionName = value.ToString()!;

            // Check if length is greater than 10
            if (lionName.Length <= 3)
            {
                return new ValidationResult("Lion Name must be greater than 3 characters.");
            }

            // Split into words and check each word
            string[] words = lionName.Split(' ');
            foreach (var word in words)
            {
                if (!Regex.IsMatch(word, @"^[A-Z][a-zA-Z0-9]*$"))
                {
                    return new ValidationResult("Each word in Lion Name must begin with a capital letter.");
                }
            }

            // Check for forbidden special characters
            if (Regex.IsMatch(lionName, @"[^a-zA-Z0-9]"))
            {
                return new ValidationResult("Lion Name must not contain special characters such as #, @, &, (, ).");
            }

            return ValidationResult.Success;
        }
    }
}
