using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class AlphabeticAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string stringValue = value.ToString();
            if (!stringValue.All(char.IsLetter))
            {
                return new ValidationResult("The field must contain only alphabetical characters.");
            }
        }

        return ValidationResult.Success;
    }
}
