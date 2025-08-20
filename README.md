# Question

### Objective
You will be given a list where each element of the list is a string that contains JSON data. Example:
```json
{"name": "John","age": 35,"height": 180,"city": "London"}
```

Each JSON object will always have a name field, but none of the other fields are required to be in the entry. Example:
```json
{"name": "Sarah","city": "Paris"}
```
or even
```json
{"name": "Emily"}
```

There might be multiple entries for the same person that contain different data fields. Example:
```json
{"name": "Sam","age": 31}
```
and later
```json
{"name": "Sam", "city": "Dublin"}
```

There will never be a JSON object that contains data that contradicts a previous entry.

### Instructions
1. **Data Processing**: Given this list, process the data to output the following:
   - A list of people who are older than 25, sorted alphabetically by name.
   - A list of all the cities in the given data and how many people in the given data live in each city.
   - Calculate the average age of the people in the dataset.
   - Identify and list any duplicate entries based on the `name` field.

2. **API Development**:
   - Create an HTTP REST API to handle this data processing.
   - Implement the following endpoints:
     - `POST /api/data`: Accepts a JSON payload containing the list of JSON strings, processes the data, and stores it in memory.
     - `GET /api/people-over-25`: Returns a JSON list of people who are older than 25, sorted alphabetically by name.
     - `GET /api/cities`: Returns a JSON object with all the cities and the count of how many people live in each city.
     - `GET /api/average-age`: Returns the average age of the people in the dataset.
     - `GET /api/duplicates`: Returns a JSON list of any duplicate entries based on the `name` field.
   
### Requirements
- Use C# for the entire implementation.
- Include instructions on how to run and test your API in a README file.
- Provide generated API documentation using an OpenAPI spec, or similar.
- Write production-ready code.

### Example API Endpoints

**POST /api/data**
Request body:
```json
[
  "{\"name\":\"John\",\"age\":35,\"height\":180,\"city\":\"London\"}",
  "{\"name\":\"Sarah\",\"city\":\"Paris\"}",
  "{\"name\":\"Emily\"}",
  "{\"name\":\"Sam\",\"age\":31}",
  "{\"name\":\"Sam\",\"city\":\"Dublin\"}"
]
```

**GET /api/people-over-25**
Response:
```json
[
  {"name": "John", "age": 35, "height": 180, "city": "London"},
  {"name": "Sam", "age": 31, "city": "Dublin"}
]
```

**GET /api/cities**
Response:
```json
{
  "London": 1,
  "Paris": 1,
  "Dublin": 1
}
```

**GET /api/average-age**
Response:
```json
{
  "averageAge": 33
}
```

**GET /api/duplicates**
Response:
```json
[
  {"name": "Sam", "age": 31},
  {"name": "Sam", "city": "Dublin"}
]
```

### Evaluation Criteria
- Correctness and completeness of the implemented API and data processing.
- Code quality and adherence to best practices.
- Proper error handling and input validation.
- Clarity and completeness of the documentation.

We look forward to reviewing your submission. Good luck!

 
 
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

6. To run the tests, use the command:
   
	dotnet test
7. To populate the data - Send a POST request to /api/data with a JSON array of people:
```json
[
  "{\"name\":\"John\",\"age\":35,\"height\":180,\"city\":\"London\"}",
  "{\"name\":\"Sarah\",\"city\":\"Paris\"}",
  "{\"name\":\"Emily\"}",
  "{\"name\":\"Sam\",\"age\":31}",
  "{\"name\":\"Sam\",\"city\":\"Dublin\"}"
]
```
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
- NUnit for testing
- Swagger for API documentation

## Results

https://github.com/sairamk22/Assignment/blob/master/Swagger_Results.mp4

If you encounter any issues running the code, feel free to reach out
Cheers,
Sai Ram Kaleru
Contact: sairamvanjari@gmail.com

