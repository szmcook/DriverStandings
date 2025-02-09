using DriverStandingsApi.Models;

namespace DriverStandingsApi.Services
{
    public interface IDriverStandingsService
    {
        Task<List<FormattedDriver>> GetFormattedDrivers(string year);
    }
}