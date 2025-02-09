using System.Text.Json;
using DriverStandingsApi.Models;

namespace DriverStandingsApi.Services
{
    public class DriverStandingsService(ILogger<DriverStandingsService> logger, IHttpClientFactory httpClientFactory) : IDriverStandingsService
    {
        private readonly ILogger<DriverStandingsService> _logger = logger;
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("RBApi");
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<List<FormattedDriver>> GetFormattedDrivers(string year)
        {
            try
            {
                // make api call
                var response = await _httpClient.GetStringAsync(year);
                var drivers = JsonSerializer.Deserialize<List<Driver>>(response, _jsonSerializerOptions);

                _logger.LogInformation("Successfully received response from web api");

                if (drivers == null) return [];

                return drivers
                .OrderByDescending(d => d.SeasonPoints)
                .Select((d, index) => new FormattedDriver
                {
                    Name = d.FirstName + " " + d.LastName,
                    DriverCountryCode = d.DriverCountryCode,
                    SeasonTeamName = d.SeasonTeamName,
                    SeasonPoints = d.SeasonPoints,
                    Position = index + 1
                })
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving driver standings");
                return new List<FormattedDriver>();
            }
        }

    }
}

