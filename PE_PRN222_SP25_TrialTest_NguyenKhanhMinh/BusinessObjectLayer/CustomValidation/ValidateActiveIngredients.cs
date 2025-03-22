using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusinessObjectLayer.CustomValidation
{
    public class ValidateActiveIngredients : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("ActiveIngredients is required.");
            }

            string activeIngredients = value.ToString()!;

            // Check if length is greater than 10
            if (activeIngredients.Length <= 10)
            {
                return new ValidationResult("ActiveIngredients must be greater than 10 characters.");
            }

            // Split into words and check each word
            string[] words = activeIngredients.Split(' ');
            foreach (var word in words)
            {
                if (!Regex.IsMatch(word, @"^[A-Z0-9][a-zA-Z0-9]*$"))
                {
                    return new ValidationResult("Each word in ActiveIngredients must begin with a capital letter or a number and contain no special characters.");
                }
            }

            // Check for forbidden special characters
            if (Regex.IsMatch(activeIngredients, @"[#@&()]"))
            {
                return new ValidationResult("ActiveIngredients must not contain special characters such as #, @, &, (, ).");
            }

            return ValidationResult.Success;
        }
    }
}
