using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace LocalFriendzApi.Tests.Endpoints
{
    public class ContactEndpointsTests
    {
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [Test]
        public async Task GetContactById_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();
            var contactId = Guid.NewGuid();

            // Act
            var response = await client.GetAsync($"/api/contacts/{contactId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
