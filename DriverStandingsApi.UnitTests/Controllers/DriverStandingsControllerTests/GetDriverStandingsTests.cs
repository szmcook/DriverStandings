using DriverStandingsApi.Services;
using Moq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using DriverStandingsApi.Controllers;
using DriverStandingsApi.Models;

namespace DriverStandingsApi.UnitTests.Controllers.DriverStandingsControllerTests
{
    public class GetDriverStandingsTests
    {
        private readonly IFixture _fixture;

        public GetDriverStandingsTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        }

        [Fact]
        public async void ReturnsOk_When_ServiceWorks()
        {
            // Arrange
            string year = "2024";

            var expectedResult = _fixture.Create<List<FormattedDriver>>();

            _fixture.Freeze<Mock<IDriverStandingsService>>()
                .Setup(s => s.GetFormattedDrivers(year))
                .ReturnsAsync(expectedResult);

            var sut = _fixture.Build<DriverStandingsController>().OmitAutoProperties().Create();

            // Act 
            var result = await sut.GetDriverStandings(year);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultObj = Assert.IsType<List<FormattedDriver>>(okResult.Value);
            Assert.Equal(expectedResult, resultObj);
        }

        [Fact]
        public async void ReturnsBadRequest_When_ServiceReturnsEmpty()
        {
            // Arrange
            string year = "2024";

            _fixture.Freeze<Mock<IDriverStandingsService>>()
                .Setup(s => s.GetFormattedDrivers(year))
                .ReturnsAsync(new List<FormattedDriver>());

            var sut = _fixture.Build<DriverStandingsController>().OmitAutoProperties().Create();

            // Act 
            var result = await sut.GetDriverStandings(year);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ReturnsBadRequest_When_YearMissing()
        {
            string year = "invalid";

            _fixture.Freeze<Mock<IDriverStandingsService>>()
                .Setup(s => s.GetFormattedDrivers(year))
                .ThrowsAsync(new ArgumentException());

            var sut = _fixture.Build<DriverStandingsController>().OmitAutoProperties().Create();

            // Act
            var result = await sut.GetDriverStandings(year);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public async void ReturnsBadRequest_When_ExceptionOccurs()
        {
            string year = "invalid";

            _fixture.Freeze<Mock<IDriverStandingsService>>()
                .Setup(s => s.GetFormattedDrivers(year))
                .ThrowsAsync(new Exception());

            var sut = _fixture.Build<DriverStandingsController>().OmitAutoProperties().Create();

            // Act
            var result = await sut.GetDriverStandings(year);

            // Assert
            var serverErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, serverErrorResult.StatusCode);
            Assert.NotNull(serverErrorResult.Value);
            Assert.Contains("An internal server error occurred", serverErrorResult.Value.ToString());
        }
    }
}
