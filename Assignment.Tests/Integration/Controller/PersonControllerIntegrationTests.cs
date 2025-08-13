using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;

namespace Assignment.Tests.Integration.Controller
{
    [TestFixture]
    public class PersonControllerIntegrationTests
    {
        private HttpClient _client;

        private readonly string[] _jsonData = new[]
        {
            "{\"name\":\"John\",\"age\":35,\"height\":180,\"city\":\"London\"}",
            "{\"name\":\"Sarah\",\"city\":\"Paris\"}",
            "{\"name\":\"Emily\"}",
            "{\"name\":\"Sam\",\"age\":31}",
            "{\"name\":\"Sam\",\"city\":\"Dublin\"}"
        };

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [Test]
        public async Task PostData_WithValidJson_ShouldReturnOk()
        {
            var response = await _client.PostAsJsonAsync("/api/data", _jsonData);
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task PostData_WithEmptyList_ShouldReturnBadRequest()
        {
            var response = await _client.PostAsJsonAsync("/api/data", new string[] { });
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetPeopleOver25_ShouldReturnExpectedCount()
        {
            await _client.PostAsJsonAsync("/api/data", _jsonData);
            var response = await _client.GetAsync("/api/people-over-25");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var content = JObject.Parse(jsonString);
            var people = content["people"]?.ToObject<List<JObject>>();

            people.Should().NotBeNull();
            people.Should().HaveCount(2);
        }


        [Test]
        public async Task GetAverageAge_ShouldReturnExpectedValue()
        {
            await _client.PostAsJsonAsync("/api/data", _jsonData);
            var response = await _client.GetAsync("/api/average-age");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var content = JObject.Parse(jsonString);
            var averageAge = (int?)content["averageAge"];

            averageAge.Should().Be(33);
        }

        [Test]
        public async Task GetCities_ShouldReturnExpectedCounts()
        {
            await _client.PostAsJsonAsync("/api/data", _jsonData);
            var response = await _client.GetAsync("/api/cities");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var content = JObject.Parse(jsonString);
            var cityCounts = content["cityCounts"]?.ToObject<Dictionary<string, int>>();

            cityCounts.Should().NotBeNull();
            cityCounts.Should().HaveCount(3);
            cityCounts.Should().Contain(new KeyValuePair<string, int>("London", 1));
            cityCounts.Should().Contain(new KeyValuePair<string, int>("Paris", 1));
            cityCounts.Should().Contain(new KeyValuePair<string, int>("Dublin", 1));
        }

        [Test]
        public async Task GetDuplicates_ShouldReturnExpectedEntries()
        {
            await _client.PostAsJsonAsync("/api/data", _jsonData);
            var response = await _client.GetAsync("/api/duplicates");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var content = JObject.Parse(jsonString);
            var duplicates = content["duplicates"]?.ToObject<List<JObject>>();

            duplicates.Should().NotBeNull();
            duplicates.Should().HaveCount(2);
            duplicates.Should().AllSatisfy(p => p["name"]?.ToString().Should().Be("Sam"));
        }

        [TearDown]
        public void TearDown() => _client?.Dispose();
    }
}