using Assignment.Models;
using static Assignment.Services.PeopleService;
using System.Linq;
using System.Text.Json;

namespace Assignment.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly object _lock = new();
        private List<Person> _people = new();
        private readonly ILogger<PeopleService> _logger;

        public PeopleService(ILogger<PeopleService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Loads data from a collection of JSON strings representing people.
        /// </summary>
        ///<param name="jsonStrings">A collection of JSON strings, each representing a person.</param>
        public void LoadData(IEnumerable<string> jsonStrings)
        {
            var people = new List<Person>();
            _logger.LogInformation("Loading data with {Count} entries.", jsonStrings.Count());
            foreach (var json in jsonStrings)
            {
                try
                {
                    var person = JsonSerializer.Deserialize<Person>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (person?.Name == null)
                        throw new ArgumentException("Each person must have a name.");

                    people.Add(person);
                }
                catch (JsonException ex)
                {
                    _logger.LogWarning(ex, "Failed to deserialize JSON: {Json}", json);
                    throw new ArgumentException($"Invalid JSON format: {ex.Message}", ex);
                }
            }
            lock (_lock)
            {
                _people = people;
            }
        }

        /// <summary>
        /// Retrieves a list of people over the age of 25, merging entries with the same name.
        /// </summary>
        /// <returns>A sorted list of merged Person objects over age 25</returns>
        public IEnumerable<Person> GetPeopleOver25()
        {
            lock (_lock)
            {
                return _people
                    .Where(p => p.Age.HasValue && p.Age > 25)
                    .GroupBy(p => p.Name)
                    .Select(g => MergePersonEntries(g))
                    .OrderBy(p => p.Name);
            }
        }

        /// <summary>
        /// Counts the number of distinct people in each city.
        /// </summary>
        /// <returns>A dictionary mapping city names to the count of unique people</returns>
        public Dictionary<string, int> GetCityCounts()
        {
            lock (_lock)
            {
                return _people
                    .Where(p => !string.IsNullOrEmpty(p.City))
                    .GroupBy(p => p.City!)
                    .ToDictionary(g => g.Key, g => g.Select(p => p.Name).Distinct().Count());
            }
        }

        /// <summary>
        /// Calculates the average age of people, merging entries with the same name.
        /// </summary>
        /// <returns>The average age as a double, or null if no age data is available</returns>
        public double? GetAverageAge()
        {
            lock (_lock)
            {
                var ages = _people
                    .Where(p => p.Age.HasValue)
                    .GroupBy(p => p.Name)
                    .Select(g => MergePersonEntries(g))
                    .Where(p => p.Age.HasValue)
                    .Select(p => p.Age!.Value);

                if (!ages.Any()) return null;
                return ages.Average();
            }
        }

        /// <summary>
        /// Finds duplicate entries based on the name of the person.
        /// </summary>
        /// <returns>>A collection of groups, each containing duplicate Person entries</returns>
        public IEnumerable<IEnumerable<Person>> GetDuplicates()
        {
            lock (_lock)
            {
                return _people
                    .GroupBy(p => p.Name)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.AsEnumerable());
            }
        }

        /// <summary>
        /// Merges multiple Person entries with the same name into a single entry.
        /// </summary>
        /// <param name="entries">A group of Person entries with the same name.</param>
        /// <returns>A single merged Person object.</returns>
        private Person MergePersonEntries(IEnumerable<Person> entries)
        {
            var name = entries.First().Name;
            int? age = null;
            int? height = null;
            string? city = null;

            foreach (var entry in entries)
            {
                if (entry.Age.HasValue) age = entry.Age;
                if (entry.Height.HasValue) height = entry.Height;
                if (!string.IsNullOrEmpty(entry.City)) city = entry.City;
            }

            return new Person(name, age, height, city);
        }
    }
}
