using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using my_travels.Models;
using my_travels.Services;
using System.Net;

namespace my_travels.Functions;

public class GetCountries
{
    private readonly VisitedCountriesService _visitedCountriesService;
    private readonly ILogger<GetCountries> _logger;

    public GetCountries(VisitedCountriesService visitedCountriesService, ILogger<GetCountries> logger)
    {
        _visitedCountriesService = visitedCountriesService;
        _logger = logger;
    }

    [Function("GetCountries")]
    public async Task<HttpResponseData> Run(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "countries")] HttpRequestData req)
    {
        var countries = await _visitedCountriesService.GetAllAsync();

        if (countries == null || !countries.Any())
        {
            var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            await notFoundResponse.WriteStringAsync("Inga länder hittades.");
            return notFoundResponse;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(countries);
        return response;
    }
}