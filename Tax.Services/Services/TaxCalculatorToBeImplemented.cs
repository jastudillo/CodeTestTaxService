
namespace Tax.Services.Services
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Tax.Common.Web;
    using Tax.Models.Models;
    using Tax.Services.Services.Interfaces;

    public class TaxCalculatorToBeImplemented : ITaxCalculator
    {
        /// <summary>
        /// The api key.
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        /// The api key.
        /// </summary>
        private readonly string _apiUrl;

        public TaxCalculatorToBeImplemented(IConfiguration configuration)
        {
            this._apiKey = configuration.GetSection("TaxJar").GetSection("ApiKey").Value;
            this._apiUrl = configuration.GetSection("TaxJar").GetSection("Url").Value;
        }


        public async Task<CustomActionResult<decimal>> GetTaxForOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomActionResult<decimal>> GetTaxRateForLocation(Location address)
        {
            throw new NotImplementedException();
        }
    }
}
