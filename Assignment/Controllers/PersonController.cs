using Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api")]
    public class PersonController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPeopleService peopleService, ILogger<PersonController> logger)
        {
            _peopleService = peopleService;
            _logger = logger;
        }

        /// <summary>
        /// Loads a collection of JSON strings representing people into the service.
        /// </summary>
        /// <param name="jsonStrings">A list of JSON strings, each representing a person.</param>
        /// <returns>200 OK if successful; 400 Bad Request if input is invalid or deserialization fails.</returns>
        [HttpPost("data")]
        public IActionResult PostData([FromBody] IEnumerable<string> jsonStrings)
        {
            if (jsonStrings?.Any() != true)
            {
                _logger.LogWarning("Received a request to load data, but the input list was empty.");
                return BadRequest("Input list cannot be empty.");
            }
            try
            {
                _peopleService.LoadData(jsonStrings);
                _logger.LogInformation("Loaded people data from the request.");
                return Ok(new { message = "Data loaded successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while loading people data.");
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a list of people over the age of 25.
        /// </summary>
        /// <returns>200 OK with people list; 204 No Content if no matching entries found.</returns>
        [HttpGet("people-over-25")]
        public IActionResult GetPeopleOver25()
        {
            var people = _peopleService.GetPeopleOver25();
            return people?.Any() == true
                ? Ok(new { people })
                : NoContent();
        }

        /// <summary>
        /// Retrieves a count of distinct people in each city.
        /// </summary>
        /// <returns>200 OK with city counts; 204 No Content if no data is available.</returns>
        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            var cityCounts = _peopleService.GetCityCounts();
            return cityCounts?.Any() == true
                ? Ok(new { cityCounts })
                : NoContent();
        }

        /// <summary>
        /// Calculates the average age of all people in the dataset.
        /// </summary>
        /// <returns>200 OK with average age; 204 No Content if no age data is available.</returns>
        [HttpGet("average-age")]
        public IActionResult GetAverageAge()
        {
            var avg = _peopleService.GetAverageAge();
            return avg.HasValue
                ? Ok(new { averageAge = (int)Math.Round(avg.Value) })
                : NoContent();
        }

        /// <summary>
        /// Retrieves duplicate entries based on name.
        /// </summary>
        /// <returns>200 OK with duplicate entries; 204 No Content if no duplicates found.</returns>
        [HttpGet("duplicates")]
        public IActionResult GetDuplicates()
        {
            var duplicates = _peopleService.GetDuplicates()?.SelectMany(x => x).ToList();
            return duplicates?.Any() == true
                ? Ok(new { duplicates })
                : NoContent();
        }
    }
}