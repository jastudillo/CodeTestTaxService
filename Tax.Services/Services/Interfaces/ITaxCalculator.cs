
/// <summary>
/// The ITaxCalculator Interface File.
/// </summary>
namespace Tax.Services.Services.Interfaces
{

    using System.Threading.Tasks;
    using Tax.Common.Web;
    using Tax.Models.Models;

    /// <summary>
    /// The ITaxCalculator Interface.
    /// </summary>
    public interface ITaxCalculator
    {
        /// <summary>
        /// The GetTaxRateForLocation.
        /// Gets the tax rate for a given location .
        /// </summary>
        /// <param name="location">
        /// The location for the tax to be calculated.
        /// </param>
        /// <returns>
        /// The <see cref="Task{decimal}"/>.
        /// </returns>
        Task<CustomActionResult<decimal>> GetTaxRateForLocation(Location location);

        /// <summary>
        /// The GetTaxForOrder.
        /// Get the taxes for a given order.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The <see cref="Task{decimal}"/>.
        /// </returns>
        Task<CustomActionResult<decimal>> GetTaxForOrder(Order order);
    }
}
