/// <summary>
/// The BaseController file name.
/// </summary>

namespace Tax.API.Controllers.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Tax.Common.Web;
    using Tax.Common.Web.Interface;
    using Enum = Tax.Common.Web.Enum;

    /// <summary>
    /// The BaseController class.
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// The ClientName property pass from header to define client name.
        /// </summary>  
        protected string ClientName
        {
            get
            {
                return HttpContext.Request.Headers["ClientName"];
            }
        }

        /// <summary>
        /// The Ok Method that returns the result for API request.
        /// </summary>
        protected ObjectResult OK<T>(T value)
        {
            if (value == null)
            {
                return (ObjectResult)new NotFoundObjectResult(null);
            }
            else
            {
                var valueType = value.GetType();
                var actionResultStatusType = typeof(ICustomActionResult);

                if (actionResultStatusType.IsAssignableFrom(valueType))
                {
                    var dto = (ICustomActionResult)value;

                    if (dto.StatusCode == Enum.StatusCode.NotFound)
                    {
                        return this.NotFound(dto.Message);
                    }

                    if (dto.StatusCode == Enum.StatusCode.Error)
                    {
                        return this.BadRequest(dto.Message);
                    }

                }
                return base.Ok(value);
            }
        }
    }
}