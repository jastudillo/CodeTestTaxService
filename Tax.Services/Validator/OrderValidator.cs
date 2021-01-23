
namespace Tax.Services.Validator
{
    using System.Linq;
    using Tax.Models.Models;

    /// <summary>
    /// The order validator extension class.
    /// </summary>
    public static class OrderValidator
    {
        /// <summary>
        /// Check whether the model is valid
        /// </summary>
        /// <param name="order">the order model</param>
        /// <param name="message">the validation message return</param>
        /// <returns>true for valid model, false for invalid</returns>
        public static bool IsValid(this Order order, out string message)
        {
           if (string.IsNullOrEmpty(order.ToCountry))
            {
                message = "To Country is required";
                return false;
            }
             else if (!string.IsNullOrEmpty(order.ToCountry) && (order.ToCountry.Length != 2 || order.ToCountry.Any(char.IsDigit)))
            {
                message = "To Country must be 2 letter ISO";
                return false;
            }
            else if (!string.IsNullOrEmpty(order.ToCountry) && order.ToCountry == "US" && string.IsNullOrEmpty(order.ToZip))
            {
                message = "To Zip is required when To Country is US";
                return false;
            }
            else if (!string.IsNullOrEmpty(order.ToCountry) && order.ToCountry == "US" && string.IsNullOrEmpty(order.ToState))
            {
                message = "To State is required when To Country is US";
                return false;
            }
            else if (!string.IsNullOrEmpty(order.ToState) && (order.ToState.Length != 2 || order.ToState.Any(char.IsDigit)))
            {
                message = "To State must be a 2 letter ISO";
                return false;
            }
            else
            {
                message = "Valid";
                return true;
            }
           
        }
    }
}
