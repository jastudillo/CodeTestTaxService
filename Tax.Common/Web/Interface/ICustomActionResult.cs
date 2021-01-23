/// <summary>
/// The ICustomActionResult interface file.
/// </summary>

namespace Tax.Common.Web.Interface
{
    /// <summary>
    /// The ICustomActionResult interface.
    /// </summary>
    public interface ICustomActionResult 
    {
        /// <summary>
        /// The Message property.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// The StatusCode property.
        /// </summary>
        Enum.StatusCode StatusCode { get; set; }
    }
}
