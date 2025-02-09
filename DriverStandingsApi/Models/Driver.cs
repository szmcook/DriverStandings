using System.Text.Json.Serialization;

namespace DriverStandingsApi.Models
{
    public class Driver
    {
        [JsonPropertyName("driver_uuid")]
        public string DriverUuid { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("driver_country_code")]
        public string DriverCountryCode { get; set; } = string.Empty;

        [JsonPropertyName("driver_image")]
        public string DriverImage { get; set; } = string.Empty;

        [JsonPropertyName("team_uuid")]
        public string TeamUuid { get; set; } = string.Empty;

        [JsonPropertyName("season_team_name")]
        public string SeasonTeamName { get; set; } = string.Empty;

        [JsonPropertyName("season_points")]
        public int SeasonPoints { get; set; } = 0;
    }
}
