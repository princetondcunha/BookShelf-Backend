using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Bookshelf.Models
{
    public class Address : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }

        [Required]
        [RegularExpression("^(Canada|US)$", ErrorMessage = "Country should be Canada or US.")]
        public string Country { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public User? User { get; set; }

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
            string canadianPostalCodePattern = @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$";
            return Regex.IsMatch(postalCode, canadianPostalCodePattern);
        }
        private bool IsValidUSZipCode(string zipCode)
        {
            string usZipCodePattern = @"^\d{5}(?:-\d{4})?$";
            return Regex.IsMatch(zipCode, usZipCodePattern);
        }
    }
}
