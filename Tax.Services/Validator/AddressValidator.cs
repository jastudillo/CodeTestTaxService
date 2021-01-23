using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tax.Models.Models;

namespace Tax.Services.Validator
{
    /// <summary>
    /// The location validator extension class.
    /// </summary>
    public static class LocationValidator
    {
        /// <summary>
        /// Check whether the model is valid
        /// </summary>
        /// <param name="location">the location model</param>
        /// <param name="message">the validation message return</param>
        /// <returns>true for valid model, false for invalid</returns>
        public static bool IsValid(this Location location, out string message)
        {
            if (string.IsNullOrEmpty(location.Zip))
            {
                message = "Zip is required";
                return false;
            }
            else if (!string.IsNullOrEmpty(location.Country) && (location.Country.Length != 2 || location.Country.Any(char.IsDigit)))
            {
                message = "Country must be 2 letter ISO";
                return false;
            }
            else if (!((location.Zip.Length == 5 && location.Zip.All(char.IsDigit)) ||
                (location.Zip.Length == 10 && location.Zip.Substring(0, 5).All(char.IsDigit) && location.Zip.Substring(5, 1) == "-" && location.Zip.Substring(6, 4).All(char.IsDigit))) && string.IsNullOrEmpty(location.Country))
            {
                message = "Country is required for non US Locations";
                return false;
            }
            message = "Valid";
            return true;
        }
    }
}
