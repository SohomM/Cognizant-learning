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




### ---------------------------------------------------------------------------------------------

Lab – Web API with Swagger & Postman in .NET Core

## 1. Web API using .NET Core with Swagger
🔹 Step 1: Create or Open .NET Core Web API Project

Using an existing API project or create a new one:

```bash
dotnet new webapi -n SwaggerDemoApi
cd SwaggerDemoApi
```

🔹 Step 2: Install Swagger Package

Installed **Swashbuckle.AspNetCore** NuGet package:

```bash
dotnet add package Swashbuckle.AspNetCore
```

🔹 Step 3: Modify `Startup.cs`

#### In `ConfigureServices` method:

```csharp
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Swagger Demo",
        Version = "v1",
        Description = "TBD",
        TermsOfService = new Uri("https://www.example.com"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "John Doe",
            Email = "john@xyzmail.com",
            Url = new Uri("https://www.example.com")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "License Terms",
            Url = new Uri("https://www.example.com")
        }
    });
});
```

#### In `Configure` method:

```csharp
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
});
```

Make sure to also include:

```csharp
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
```

🔹 Step 4: Run the Application

```bash
dotnet run
```

Navigate to:

```
https://localhost:[port]/swagger
```

✅ Expected Swagger UI Output:

* Title: Swagger Demo
* Version: v1
* Contact: John Doe, [john@xyzmail.com](mailto:john@xyzmail.com)
* List of all controller methods
* Click `GET` (ValuesController) → Try it out → Execute → Shows sample response


## 2. Test Web API using POSTMAN
🔹 Step 1: Create a New Controller – EmployeeController.cs

```csharp
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var employees = new List<string> { "Ram", "Sita", "Lakshman" };
        return Ok(employees);
    }
}
```

🔹 Step 2: Run the API

```bash
dotnet run
```

API URL:

```
https://localhost:[port]/employee
```

🔹 Step 3: Use Postman

* Open Postman
* Set method to `GET`
* Enter the URL: `https://localhost:[port]/employee`
* Click **Send**

✅ Expected Output in Postman:

* **Body (JSON):**

```json
[
  "Ram",
  "Sita",
  "Lakshman"
]
```

* **Status:** 200 OK

🔹 Step 4: Modify Route Attribute to `Emp`

Update controller route:

```csharp
[Route("emp")]
public class EmployeeController : ControllerBase
```

Run again and test in Postman:

```
GET https://localhost:[port]/emp
```

# ----------------------------------------------------------------------
Web API – Custom Model, Authorization Filter, and Exception Filter


## 1. Web API using Custom Model Class
🔹 Step 1: Create Model Classes

**Models/Department.cs**

```csharp
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

**Models/Skill.cs**

```csharp
public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

**Models/Employee.cs**

```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Salary { get; set; }
    public bool Permanent { get; set; }
    public Department Department { get; set; }
    public List<Skill> Skills { get; set; }
    public DateTime DateOfBirth { get; set; }
}
```

🔹 Step 2: Create Controller

**Controllers/EmployeeController.cs**

```csharp
using Microsoft.AspNetCore.Mvc;
using SwaggerDemoApi.Models;
using SwaggerDemoApi.Filters;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(CustomAuthFilter))]
[TypeFilter(typeof(CustomExceptionFilter))]
public class EmployeeController : ControllerBase
{
    private List<Employee> GetStandardEmployeeList()
    {
        return new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John",
                Salary = 50000,
                Permanent = true,
                Department = new Department { Id = 1, Name = "HR" },
                Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" }, new Skill { Id = 2, Name = "SQL" } },
                DateOfBirth = new DateTime(1990, 01, 01)
            }
        };
    }

    [HttpGet("standard")]
    public ActionResult<Employee> GetStandard()
    {
        var emp = GetStandardEmployeeList().FirstOrDefault();
        return Ok(emp);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Employee>), 200)]
    [ProducesResponseType(500)]
    public ActionResult<List<Employee>> Get()
    {
        throw new Exception("This is a test exception");
        // return Ok(GetStandardEmployeeList());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Employee emp)
    {
        return Ok(emp);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Employee emp)
    {
        return Ok(emp);
    }
}
```


## 2. Custom Action Filter for Authorization
🔹 Step 1: Create Filter

**Filters/CustomAuthFilter.cs**

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SwaggerDemoApi.Filters
{
    public class CustomAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                context.Result = new BadRequestObjectResult("Invalid request - No Auth token");
                return;
            }

            if (!token.ToString().Contains("Bearer"))
            {
                context.Result = new BadRequestObjectResult("Invalid request - Token present but Bearer unavailable");
            }
        }
    }
}
```


## 3. Custom Exception Filter

🔹 Step 1: Create Filter

**Filters/CustomExceptionFilter.cs**

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SwaggerDemoApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            Directory.CreateDirectory(logPath);
            var filePath = Path.Combine(logPath, "errorlog.txt");
            File.AppendAllText(filePath, $"{DateTime.Now} - {exception.Message}{Environment.NewLine}");

            context.Result = new ObjectResult("An unexpected error occurred")
            {
                StatusCode = 500
            };
        }
    }
}
```

## 4. NuGet Package

Install WebApiCompatShim if not already:

```bash
dotnet add package Microsoft.AspNet.WebApi.CompatibilityShim
```


## 5. Swagger Test

Run the app:

```bash
dotnet run
```

Navigate to:

```
https://localhost:[port]/swagger
```

* Observe:

  * GET methods show return types (200 and 500)
  * Try the GET endpoint → it triggers an exception
  * Check `Logs/errorlog.txt` for logged exception

