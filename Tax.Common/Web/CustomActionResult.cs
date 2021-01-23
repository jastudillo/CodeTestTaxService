
namespace Tax.Common.Web
{
    using Tax.Common.Web.Interface;

    /// <summary>
    /// The CustomActionResult class to be return for api calls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomActionResult<T> : ICustomActionResult
    {
        /// <summary>
        ///  The Result for the request
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// The Message of Result
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The status code of results
        /// </summary>
        public Enum.StatusCode StatusCode { get; set; }
    }
}
