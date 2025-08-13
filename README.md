# Assignment API

This project is a simple yet robust ASP.NET Core Web API designed to process and analyze person data from a list of JSON strings. It supports loading data, filtering people over age 25, calculating average age, counting cities, and identifying duplicates.

---

## Objective

You’ll be working with a list of JSON strings, each representing a person. Some entries may contain only a name, while others may include age, height, or city. Multiple entries for the same person may appear, each contributing different pieces of information.

The API processes this data and provides endpoints to:

- List people over age 25
- Count how many people live in each city
- Calculate the average age
- Identify duplicate entries based on name

## How to Run the API

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet8.0) or later

### Steps

1. Open the project folder in your terminal or IDE.

2. Restore dependencies:

   dotnet restore

3. Run the application:
   	
    dotnet run

4. The Swagger API will be available at `https://localhost:7114/swagger/index.html`

5. Test the API using Swagger UI or any HTTP client like Postman.

6. To Run the tests, use the command:
   
	dotnet test
7. To populate the data - Send a POST request to /api/data with a JSON array of people:

[
  "{\"name\":\"John\",\"age\":35,\"height\":180,\"city\":\"London\"}",
  "{\"name\":\"Sarah\",\"city\":\"Paris\"}",
  "{\"name\":\"Emily\"}",
  "{\"name\":\"Sam\",\"age\":31}",
  "{\"name\":\"Sam\",\"city\":\"Dublin\"}"
]

8. To list people over age 25 -  Send a GET request to /api/people-over-25

9. To count how many people live in each city -  Send a GET request to /api/cities

10. To calculate the average age -  Send a GET request to /api/average-age
 
11. To identify duplicate entries based on name - -  Send a GET request to /api/duplicates
 
### Integration Tests Coverage

Data Loading
Filtering By age
City Counts
Average age Calculation
Duplicate Detection

###  How to Stop the API

- If running in Visual Studio:
Press Shift + F5 to stop the application.
- If running from terminal:
Press Ctrl + C to terminate the process.

## Technologies Used

- ASP.NET Core Web API
- C#
- xUnit for testing
- Swagger for API documentation

If you encounter any issues running the code, feel free to reach out
Cheers,
Sai Ram Kaleru
Contact: sairamvanjari@gmail.com

