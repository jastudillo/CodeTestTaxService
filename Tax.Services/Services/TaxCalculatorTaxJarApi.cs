
namespace Tax.Services.Services
{
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Authenticators;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
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

        public TaxCalculatorTaxJarApi(IConfiguration configuration, IMapper mapper)
        {
            this._apiKey = configuration.GetSection("TaxJar").GetSection("ApiKey").Value;
            this._apiUrl = configuration.GetSection("TaxJar").GetSection("Url").Value;
            this._rounding = configuration.GetSection("Client").GetSection("Rounding").Value;
            this._mapper = mapper;
        }

        public async Task<CustomActionResult<decimal>> GetTaxForOrder(Models.Models.Order order)
        {
            string validMessage = string.Empty;
            var isModelValid = order.IsValid(out validMessage);

            if (isModelValid)
            {
                var orderApi = _mapper.Map<Taxjar.Order>(order);

                var client = new RestClient(_apiUrl) { Authenticator = new JwtAuthenticator(this._apiKey) };
                var request =
                    new RestRequest("taxes")
                    {
                        Method = Method.POST,
                        RequestFormat = DataFormat.Json
                    };
                request.AddJsonBody(JsonConvert.SerializeObject(orderApi));
                var response = await client.ExecutePostAsync(request);

                if (response.ResponseStatus != ResponseStatus.Completed || response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("An Error occurred processing the request.");
                }

                var responseData = JsonConvert.DeserializeObject<TaxResponse>(response.Content);


                return new CustomActionResult<decimal> { Result = Math.Round((decimal)responseData.Tax.AmountToCollect, this._rounding.All(Char.IsDigit) ? Convert.ToInt32(this._rounding) : 2), StatusCode = Common.Web.Enum.StatusCode.Success, Message = "Success" };
            }
            else
            {
                return new CustomActionResult<decimal> { Result = (decimal)0.00, Message = validMessage, StatusCode = Common.Web.Enum.StatusCode.Error };
            }
        }

        public async Task<CustomActionResult<decimal>> GetTaxRateForLocation(Location address)
        {
            string validMessage = string.Empty;
            var isModelValid = address.IsValid(out validMessage);

            if (isModelValid)
            {
                var request =
              new RestRequest($"rates/{address.Zip}")
              {
                  Method = Method.GET,
                  RequestFormat = DataFormat.Json
              };

                var client = new RestClient(this._apiUrl) { Authenticator = new JwtAuthenticator(this._apiKey) };

                var response = await client.ExecuteGetAsync(request);

                if (response.ResponseStatus != ResponseStatus.Completed || response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("An Error occurred processing the request.");
                }

                var responseData = JsonConvert.DeserializeObject<RateResponse>(response.Content);

                return new CustomActionResult<decimal> { Result = Math.Round((decimal)responseData.Rate.CombinedRate, this._rounding.All(Char.IsDigit) ? Convert.ToInt32(this._rounding) : 2), StatusCode = Common.Web.Enum.StatusCode.Success, Message = "Success" };
            }
            else
            {
                return new CustomActionResult<decimal> { Result = (decimal)0.00, Message = validMessage, StatusCode = Common.Web.Enum.StatusCode.Error };
            }

        }
    }
}
