using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using my_travels.DTOs;
using my_travels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace my_travels.Services
{
    public class CountryAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountryAPIService> _logger;

        public CountryAPIService(HttpClient httpClient, ILogger<CountryAPIService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Country?> FetchCountryAsync(string countryName)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<CountryAPIDto>>($"https://restcountries.com/v3.1/name/{countryName}");

                if (response == null || response.Count == 0)
                {
                    _logger.LogWarning("Hittade inget land med namn {countryName}", countryName);
                    return null;
                }

                var apiCountry = response[0];

                return new Country
                {
                    name = apiCountry.Name.Common,
                    nativeName = apiCountry.Name.NativeName?.Values.FirstOrDefault()?.Common ?? "",
                    capital = apiCountry.Capital?.FirstOrDefault() ?? "",
                    languages = apiCountry.Languages != null ? string.Join(", ", apiCountry.Languages.Values) : "",
                    flags = apiCountry.Flags.Png ?? "",
                    continents = apiCountry.Continents?.FirstOrDefault() ?? ""
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fel vid hämtning av land {countryName}", countryName);
                return null;
            }
        }
    }
}