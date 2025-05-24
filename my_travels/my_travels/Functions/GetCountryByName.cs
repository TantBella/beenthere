using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using my_travels.Services;
using System.Net;

namespace my_travels.Functions;

public class GetCountryByName
{
    private readonly ILogger<GetCountryByName> _logger;
    private readonly VisitedCountriesService _visitedCountriesService;

    public GetCountryByName(ILogger<GetCountryByName> logger, VisitedCountriesService visitedCountriesService)
    {
        _logger = logger;
        _visitedCountriesService = visitedCountriesService;
    }

    [Function("GetCountryByName")]
    public async Task<HttpResponseData> Run(
         [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "countries/{name}")] HttpRequestData req,
         string name)
    {
        _logger.LogInformation("Hämtar land med namn: {name}", name);

        var country = await _visitedCountriesService.GetByNameAsync(name);

        if (country == null)
        {
            var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            await notFoundResponse.WriteAsJsonAsync(new { message = "Landet hittades inte" });
            return notFoundResponse;
        }

        var okResponse = req.CreateResponse(HttpStatusCode.OK);
        await okResponse.WriteAsJsonAsync(country);
        return okResponse;
    }

}