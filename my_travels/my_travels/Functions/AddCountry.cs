using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using my_travels.Models;
using my_travels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace my_travels.Functions
{
    public class AddCountry
    {
        private readonly CountryAPIService _countryAPIService;
        private readonly VisitedCountriesService _visitedCountriesService;
        private readonly ILogger<AddCountry> _logger;

        public AddCountry(CountryAPIService countryAPIService, VisitedCountriesService visitedCountriesService, ILogger<AddCountry> logger)
        {
            _countryAPIService = countryAPIService;
            _visitedCountriesService = visitedCountriesService;
            _logger = logger;
        }

        [Function("AddCountry")]
        public async Task<HttpResponseData> Run(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = "countries/add/{name}")] HttpRequestData req,
      string name)
        {
            var country = await _countryAPIService.FetchCountryAsync(name);

            if (country == null)
            {
                var notFoundResponse = req.CreateResponse(System.Net.HttpStatusCode.NotFound);
                await notFoundResponse.WriteStringAsync($"Inget land hittades med namn {name}");
                return notFoundResponse;
            }

            await _visitedCountriesService.CreateAsync(country);

            var okResponse = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await okResponse.WriteAsJsonAsync(country);
            return okResponse;
        }
    }
}