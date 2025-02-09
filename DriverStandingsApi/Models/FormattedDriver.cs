namespace DriverStandingsApi.Models
{
    public class FormattedDriver
    {
        public string Name { get; set; } = string.Empty;
        public string DriverCountryCode { get; set; } = string.Empty;
        public string SeasonTeamName { get; set; } = string.Empty;
        public int SeasonPoints { get; set; } = 0;
        public int Position { get; set; } = 0;
    }
}
