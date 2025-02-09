using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using DriverStandingsApi.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace DriverStandingsApi.UnitTests.Services.DriverStandingsServiceTests
{
    public class GetFormattedDriversTests
    {
        private readonly IFixture _fixture;

        private readonly Mock<ILogger<DriverStandingsService>> _loggerMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<HttpMessageHandler> _handlerMock;

        public GetFormattedDriversTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
            _loggerMock = new Mock<ILogger<DriverStandingsService>>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _handlerMock = new Mock<HttpMessageHandler>();
        }

        [Fact]
        public async Task ReturnsFormattedList_WhenApiCallSucceed()
        {
            // Arrange
            var year = "2025";
            var apiResponse = "[{\r\n\"driver_uuid\": \"bc7cba1c-4ce6-4a17-a2a2-913ef8e1d06c\",\r\n\"first_name\": \"Max\",\r\n\"last_name\": \"Verstappen\",\r\n\"driver_country_code\": \"NL\",\r\n\"driver_image\": \"https://img.redbull.com/image/upload/{op}/redbullcom/2023/11/20/jru9szb6alc794lqjpyx\",\r\n\"team_uuid\": \"a3c53571-508a-4f1e-9e92-82f751bcc65b\",\r\n\"season_team_name\": \"Oracle Red Bull Racing\",\r\n\"season_points\": 437\r\n}]";

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(apiResponse)
                });

            var client = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.example.com/")
            };

            _httpClientFactoryMock.Setup(factory => factory.CreateClient("RBApi"))
                .Returns(client);

            var sut = new DriverStandingsService(_loggerMock.Object, _httpClientFactoryMock.Object);

            // Act
            var result = await sut.GetFormattedDrivers(year);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.First().Position);
            Assert.Equal("Max Verstappen", result.First().Name);
            Assert.Equal(437, result.First().SeasonPoints);
        }

        [Fact]
        public async Task ReturnEmptyList_WhenApiCallFails()
        {
            // Arrange
            var year = "2025";

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                });

            var client = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.example.com/")
            };

            _httpClientFactoryMock.Setup(factory => factory.CreateClient("RBApi"))
                .Returns(client);

            var sut = new DriverStandingsService(_loggerMock.Object, _httpClientFactoryMock.Object);

            // Act
            var result = await sut.GetFormattedDrivers(year);

            // Assert
            Assert.Empty(result);
        }
    }
}