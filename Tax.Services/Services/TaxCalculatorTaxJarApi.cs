
namespace Tax.Services.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Authenticators;
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Tax.Common.Web;
    using Tax.Models.Models;
    using Tax.Services.Services.Interfaces;
    using Tax.Services.Validator;
    using Taxjar;

    public class TaxCalculatorTaxJarApi : ITaxCalculator
    {
        /// <summary>
        /// The api key.
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        /// The api key.
        /// </summary>
        private readonly string _apiUrl;

        /// <summary>
        /// The rounding for rates .
        /// </summary>
        public string _rounding { get; set; }

        /// <summary>
        /// The automapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The TaxCalculatorTaxJarApi constructor.
        /// </summary>
        public TaxCalculatorTaxJarApi(IConfiguration configuration, IMapper mapper)
        {
            this._apiKey = configuration.GetSection("TaxJar").GetSection("ApiKey").Value;
            this._apiUrl = configuration.GetSection("TaxJar").GetSection("Url").Value;

            //this can be get per client in the system if multiple clients
            this._rounding = configuration.GetSection("Client").GetSection("Rounding").Value;
            this._mapper = mapper;
        }

        /// <summary>
        /// Gets the tax amount for a given order
        /// </summary>
        /// <param name="order">
        /// the order
        /// </param>
        /// <returns> the tax amount for a given order</returns>
        public async Task<CustomActionResult<decimal>> GetTaxForOrder(Models.Models.Order order)
        {
            // default result if an error ocurred
            var result = new CustomActionResult<decimal> { Result = (decimal)0.00, Message = "An Error occurred processing the request.", StatusCode = Common.Web.Enum.StatusCode.Error };
            try
            {
                string validMessage = string.Empty;
                var isModelValid = order.IsValid(out validMessage);

                if (isModelValid)
                {
                    var taxJarOrder = _mapper.Map<Taxjar.Order>(order);

                    var client = new RestClient(_apiUrl) { Authenticator = new JwtAuthenticator(this._apiKey) };
                    var request = new RestRequest("taxes")
                    {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json
                    };
                    request.AddJsonBody(JsonConvert.SerializeObject(taxJarOrder));
                    var response = await client.ExecutePostAsync(request);

                    // make sure api call return with 200
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        result.Message = "Tax Jar API did not return success code.";
                        return result;
                    }

                    var responseData = JsonConvert.DeserializeObject<TaxResponse>(response.Content);

                    result = new CustomActionResult<decimal>
                    {
                        Result = Math.Round((decimal)responseData.Tax.AmountToCollect, this._rounding.All(Char.IsDigit) ? Convert.ToInt32(this._rounding) : 2),
                        StatusCode = Common.Web.Enum.StatusCode.Success,
                        Message = "Success"
                    };

                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                // handle exception here i.e. log into server, app insights , etc

                return result;
            }
        }

        /// <summary>
        /// Gets the tax rate  for a given location
        /// </summary>
        /// <param name="location">
        /// the location
        /// </param>
        /// <returns>the tax rate as decimal</returns>
        public async Task<CustomActionResult<decimal>> GetTaxRateForLocation(Location location)
        {
            // default result if an error ocurred
            var result = new CustomActionResult<decimal> { Result = (decimal)0.00, Message = "An Error occurred processing the request.", StatusCode = Common.Web.Enum.StatusCode.Error };
            try
            {
                string validMessage = string.Empty;
                var isModelValid = location.IsValid(out validMessage);

                if (isModelValid)
                {                    
                    var request = new RestRequest($"rates/{location.Zip}")
                    {
                        Method = Method.GET,
                        RequestFormat = DataFormat.Json
                    };
                    if (!string.IsNullOrEmpty(location.Country))
                    {
                        request.AddParameter("country", location.Country);
                    }
                    if (!string.IsNullOrEmpty(location.State))
                    {
                        request.AddParameter("state", location.State);
                    }
                    if (!string.IsNullOrEmpty(location.City))
                    {
                        request.AddParameter("city", location.City);
                    }
                    if (!string.IsNullOrEmpty(location.Street))
                    {
                        request.AddParameter("street", location.Street);
                    }
                    var client = new RestClient(this._apiUrl) { Authenticator = new JwtAuthenticator(this._apiKey) };

                    var response = await client.ExecuteGetAsync(request);

                    // make sure api call return with 200
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        result.Message = "Tax Jar API did not return success code.";
                        return result;
                    }

                    var responseData = JsonConvert.DeserializeObject<RateResponse>(response.Content);
                    result = new CustomActionResult<decimal>
                    {
                        Result = Math.Round((decimal)responseData.Rate.CombinedRate, this._rounding.All(Char.IsDigit) ? Convert.ToInt32(this._rounding) : 2),
                        StatusCode = Common.Web.Enum.StatusCode.Success,
                        Message = "Success"
                    };

                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                // handle exception here i.e. log into  server, app insights , etc

                return result;
            }

        }
    }
}
