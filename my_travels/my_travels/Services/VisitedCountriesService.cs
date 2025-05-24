using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using my_travels.Models;
using System.Collections;


namespace my_travels.Services
{
    public class VisitedCountriesService
    {
        private readonly IMongoCollection<Country> _countries;

        public VisitedCountriesService(IConfiguration config)
        {
            var connectionString = config["CosmosDbConnection"];

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("DbConnection saknas");
                throw new Exception("Saknad DbConnection");
            }

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("CountryCollection");
            _countries = database.GetCollection<Country>("VisitedCountries");
        }

        public async Task CreateAsync(Country country)
        {
            await _countries.InsertOneAsync(country);
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _countries.Find(_ => true).ToListAsync();
        }

        public async Task<Country?> GetByNameAsync(string name)
        {
            var filter = Builders<Country>.Filter.Eq(c => c.name, name);
            return await _countries.Find(filter).FirstOrDefaultAsync();
        }

    }
}