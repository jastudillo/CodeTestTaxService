namespace Tax.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Tax.API.Controllers.Common;
    using Tax.Common.Web.Interface;
    using Tax.Models.Models;
    using Tax.Services.Services.Interfaces;

    [Produces("application/json")]
    [Route("[controller]")]
    public class TaxCalculatorController : BaseController
    {
        /// <summary>
        /// Interface for the tax calculator based on client name
        /// </summary>
        private readonly Func<string, ITaxCalculator> _taxCalculator;

        /// <summary>
        /// Initializes the tax controller
        /// </summary>
        /// <param name="taxCalculator">the tax calculator service to be used based on client name string</param>
        public TaxCalculatorController(Func<string, ITaxCalculator> taxCalculator)
        {
            this._taxCalculator = taxCalculator;
        }

        /// <summary>
        /// The GetTaxRateForLocation
        /// Gets the tax rate for a location.
        /// </summary>
        /// <response code="200">Returns the tax rate for given location on success</response>
        /// <response code="404">Returns on not found</response>
        /// <response code="400">Returns on error</response>
        /// <param name="country">
        /// The country.
        /// </param>
        /// <param name="zip">
        /// The zip.
        /// </param>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <param name="city">
        /// The city.
        /// </param>
        /// <param name="street">
        /// The country.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        [Route("GetTaxRateForLocation")]
        public async Task<IActionResult> GetTaxRateForLocation(string country, string zip, string state, string city, string street)
        {
            Location location = new Location
            {
                Country = country,
                City = city,
                State = state,
                Street = street,
                Zip = zip,
            };
            return this.OK(await this._taxCalculator(this.ClientName).GetTaxRateForLocation(location));
        }

        /// <summary>
        /// The GetTaxForOrder
        /// Gets the tax for a given order.
        /// </summary>
        /// <response code="200">Returns the tax for an order on success</response>
        /// <response code="404">Returns on not found</response>
        /// <response code="400">Returns on error</response>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [Route("GetTaxForOrder")]
        public async Task<IActionResult> GetTaxForOrder([FromBody]Order order)
        {
            
            return this.OK(await this._taxCalculator(this.ClientName).GetTaxForOrder(order));
        }

    }
}