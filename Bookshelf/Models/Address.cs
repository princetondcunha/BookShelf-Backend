using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bookshelf.Models
{
    public class Address : IValidatableObject
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        [StringLength(9)]
        public string ZipCode { get; set; }

        [Required]
        [RegularExpression("^(Canada|US)$", ErrorMessage = "Country should be Canada or US.")]
        public string Country { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public virtual User User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Country == "Canada" && !IsValidCanadianPostalCode(ZipCode))
            {
                yield return new ValidationResult("Invalid Canadian postal code.", new[] { nameof(ZipCode) });
            }

            if (Country == "US" && !IsValidUSZipCode(ZipCode))
            {
                yield return new ValidationResult("Invalid US zip code.", new[] { nameof(ZipCode) });
            }
        }
        private bool IsValidCanadianPostalCode(string postalCode)
        {
            // Canadian postal code format: A1A 1A1
            // Example: M5G 2C2, K8N 5W6, V6B 2E2

            string canadianPostalCodePattern = @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$";
            return Regex.IsMatch(postalCode, canadianPostalCodePattern);
        }
        private bool IsValidUSZipCode(string zipCode)
        {
            // US zip code format: 12345 or 12345-6789
            // Example: 90210, 10001-1234, 94102-3456

            string usZipCodePattern = @"^\d{5}(?:-\d{4})?$";
            return Regex.IsMatch(zipCode, usZipCodePattern);
        }
    }
}
