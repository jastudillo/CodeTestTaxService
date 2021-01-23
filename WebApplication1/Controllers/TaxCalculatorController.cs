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
        private readonly Func<string, ITaxCalculator> _taxCalculator;

        public TaxCalculatorController(Func<string, ITaxCalculator> taxCalculator)
        {
            this._taxCalculator = taxCalculator;
        }


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


        [HttpPost]
        [Route("GetTaxForOrder")]
        public async Task<IActionResult> GetTaxForOrder([FromBody]Order order)
        {
            
            return this.OK(await this._taxCalculator(this.ClientName).GetTaxForOrder(order));
        }

    }
}