using DriverStandingsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriverStandingsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverStandingsController(ILogger<DriverStandingsController> logger, IDriverStandingsService driverStandingsService) : ControllerBase
    {

        private readonly ILogger<DriverStandingsController> _logger = logger;
        private readonly IDriverStandingsService _driverStandingsService = driverStandingsService;

        [HttpGet("GetDriverStandings/{year}")]
        public async Task<IActionResult> GetDriverStandings(string year)
        {
            _logger.LogInformation("Received request to get driver standings data");

            try
            {
                var standings = await _driverStandingsService.GetFormattedDrivers(year);
                if (standings.Count > 0)
                {
                    return Ok(standings);
                }
                else
                {
                    return BadRequest("No results found");
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Argument error @{message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching driver data for year {Year}", year);
                return StatusCode(500, new { error = "An internal server error occurred. Please try again later." });
            }
        }
    }
}
