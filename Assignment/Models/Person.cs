namespace Assignment.Models;
public class Person
{
    public string Name { get; }
    public int? Age { get; }
    public int? Height { get; }
    public string? City { get; }

    public Person(string name, int? age = null, int? height = null, string? city = null)
    {
        Name = name;
        Age = age;
        Height = height;
        City = city;
    }
}
