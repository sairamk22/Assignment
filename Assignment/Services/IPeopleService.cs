using Assignment.Models;

namespace Assignment.Services
{
    public interface IPeopleService
    {
        void LoadData(IEnumerable<string> jsonStrings);
        IEnumerable<Person> GetPeopleOver25();
        Dictionary<string, int> GetCityCounts();
        double? GetAverageAge();
        IEnumerable<IEnumerable<Person>> GetDuplicates();
    }
}
