**1. Explain the concept of RESTful Web Service, Web API & Microservice**
🔸 RESTful Web Service:   REST (Representational State Transfer) is an architectural style that defines constraints for creating web services. A RESTful service uses standard HTTP methods to perform CRUD operations on resources, which are represented as URLs.

🔸 Features of REST architecture:
* **Stateless:** Each request contains all necessary information; no session state is stored on the server.
* **Client-Server Model:** Clear separation between client and server.
* **Resource-Based:** Resources are identified using URIs.
* **Uses HTTP methods:** (GET, POST, PUT, DELETE)
* **Supports multiple formats:** Not restricted to XML; can respond with JSON, XML, or plain text.
* **Cacheable:** Responses can be explicitly marked as cacheable.

🔸 Web API:
* A **Web API** is a framework for building HTTP services that can be consumed by browsers, mobile apps, desktop apps, etc.
* Web APIs in .NET Core return data (not views) using RESTful principles.
* Web API is not limited to returning XML — it supports JSON, BSON, or custom formatters.

🔸 Microservice:
* A **Microservice** is a small, independently deployable component of a larger system.
* Each microservice typically owns its own database and business logic and communicates with others using lightweight mechanisms like HTTP/REST or messaging queues.
* **Difference:** A Web API can be part of a microservice, but microservices imply modularity, autonomy, and independent scaling.

**2. Explain HttpRequest & HttpResponse**

* **HttpRequest:** Represents the incoming HTTP request. Contains headers, query parameters, method type, body (for POST/PUT), cookies, etc.
* **HttpResponse:** Represents the outgoing HTTP response from the server to the client. Contains status codes (200, 404), headers, body (data or error), and content type (like JSON or HTML).

**3. List the types of Action Verbs**

| Action Verb | Description           | Annotation     |
| ----------- | --------------------- | -------------- |
| **GET**     | Retrieves data        | `[HttpGet]`    |
| **POST**    | Creates new data      | `[HttpPost]`   |
| **PUT**     | Updates existing data | `[HttpPut]`    |
| **DELETE**  | Deletes a record      | `[HttpDelete]` |

These verbs are declared as attributes in the controller methods to map incoming HTTP requests.

**4. List the types of HttpStatusCodes used in WebAPI**

| Status Code               | Description              | Return Type Method                 |
| ------------------------- | ------------------------ | ---------------------------------- |
| `200 OK`                  | Success                  | `return Ok(data);`                 |
| `400 BadRequest`          | Client-side input error  | `return BadRequest("msg");`        |
| `401 Unauthorized`        | Not authenticated        | `return Unauthorized();`           |
| `500 InternalServerError` | Server encountered error | `return StatusCode(500, "Error");` |


**5. Demonstrate creation of a simple WebAPI – with Read, Write actions**
Structure of a Web API:

* `Controllers/ValuesController.cs`
* Inherits from `ControllerBase` or `ApiController`
* Uses attributes like `[HttpGet]`, `[HttpPost]`, etc.

---

**6. Types of Configuration Files in WebAPI**

| File                  | Purpose                                                         |
| --------------------- | --------------------------------------------------------------- |
| `Startup.cs`          | Configures services, middleware, routing, DI container.         |
| `appsettings.json`    | Stores application configuration (DB strings, API keys, etc.).  |
| `launchSettings.json` | Defines how the project launches in dev mode (profiles, ports). |
| `Route.config`        | Used in .NET Framework (4.5) for URL routing.                   |
| `WebApi.config`       | Used in .NET Framework (4.5) to configure Web API behaviors.    |



## **Practical Part: First Web API using .NET Core**

### Step 1: Create a .NET Core Web API Project

```bash
dotnet new webapi -n MyFirstWebApi
cd MyFirstWebApi
```

This scaffolds a new Web API project with a sample controller.


### Step 2: Notice `ValuesController.cs` (or `WeatherForecastController.cs`)

**File:** `Controllers/WeatherForecastController.cs`
Contains default GET method with example output.

```csharp
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild"
    };

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
```


### Step 3: Run the Web API

```bash
dotnet run
```

Output:

```bash
Building...
Now listening on: https://localhost:5001
```

### Step 4: Test the GET Action

Opening a browser:

```
GET https://localhost:5001/weatherforecast
```

A JSON data obtained like:

```json
[
  {
    "date": "2024-07-01",
    "temperatureC": 23,
    "summary": "Mild"
  },
  ...
]
```




### ---------------------------------------------------------------------------
