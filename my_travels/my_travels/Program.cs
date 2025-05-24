using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using my_travels.Services;

var builder = Host.CreateDefaultBuilder()

    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddHttpClient<CountryAPIService>();
        services.AddSingleton<VisitedCountriesService>();
    });

var host = builder.Build();

host.Run();