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




# ---------------------------------------------------------------------------------------------

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

# ------------------------------------------------------------------------------
# Web API CRUD Operation – Update Action Method

## 1. Model: Employee.cs

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

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```


## 2. Controller: EmployeeController.cs

```csharp
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private static List<Employee> _employees = new List<Employee>
    {
        new Employee
        {
            Id = 1,
            Name = "John",
            Salary = 50000,
            Permanent = true,
            Department = new Department { Id = 1, Name = "HR" },
            Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" } },
            DateOfBirth = new DateTime(1990, 01, 01)
        },
        new Employee
        {
            Id = 2,
            Name = "Jane",
            Salary = 60000,
            Permanent = false,
            Department = new Department { Id = 2, Name = "Finance" },
            Skills = new List<Skill> { new Skill { Id = 2, Name = "Excel" } },
            DateOfBirth = new DateTime(1988, 05, 15)
        }
    };

    [HttpPut("{id}")]
    public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updatedEmp)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid employee id");
        }

        var emp = _employees.FirstOrDefault(e => e.Id == id);

        if (emp == null)
        {
            return BadRequest("Invalid employee id");
        }

        // Update hardcoded employee record
        emp.Name = updatedEmp.Name;
        emp.Salary = updatedEmp.Salary;
        emp.Permanent = updatedEmp.Permanent;
        emp.Department = updatedEmp.Department;
        emp.Skills = updatedEmp.Skills;
        emp.DateOfBirth = updatedEmp.DateOfBirth;

        return Ok(emp);
    }
}
```


## 3. Test Using Swagger

1. Run the application using:

```bash
dotnet run
```

2. Open Swagger UI:

```
https://localhost:[port]/swagger
```

3. Expand PUT `/employee/{id}` method.
4. Clicked **Try it out**, entered a valid id (e.g., 1), and JSON body:

```json
{
  "id": 1,
  "name": "Updated John",
  "salary": 55000,
  "permanent": true,
  "department": {
    "id": 1,
    "name": "HR"
  },
  "skills": [
    { "id": 1, "name": "C#" },
    { "id": 3, "name": "ASP.NET" }
  ],
  "dateOfBirth": "1990-01-01"
}
```

5. Clicked **Execute**.

✅ Expected Response (Swagger / Postman):

* **Status Code:** `200 OK`
* **Body:** Updated Employee JSON

If ID is `0` or non-existent, expected:

* **Status Code:** `400 Bad Request`
* **Message:** `"Invalid employee id"`

# -----------------------------------------------------------

✅ **1. CORS Enablement for Web API Access**

🔸 What is CORS?
**Cross-Origin Resource Sharing (CORS)** is a security feature implemented by browsers to prevent unauthorized cross-origin HTTP requests. If a frontend app (like Angular or React) runs on `localhost:4200` and tries to access a Web API on `localhost:5000`, CORS policies determine if this should be allowed.

🔸 How to Enable CORS in ASP.NET Core Web API
 Install NuGet Package:

```sh
Install-Package Microsoft.AspNetCore.Cors
```
 Update `Startup.cs`

**In `ConfigureServices`:**

```csharp
services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
```

**In `Configure`:**

```csharp
app.UseCors("AllowAll");
```

✅ **2. Demonstrate Security in WebAPI (JWT Authentication)**

🔸 JSON Web Token (JWT) Authentication Flow

* User authenticates and gets a JWT from `AuthController`.
* JWT is sent in the `Authorization` header as `Bearer {token}`.
* `Authorize` attribute checks for valid token, issuer, audience, and claims.

 Update `Startup.cs` for JWT Authentication

**In `ConfigureServices`:**

```csharp
string securityKey = "mysuperdupersecret";
var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "mySystem",
        ValidAudience = "myUsers",
        IssuerSigningKey = symmetricSecurityKey
    };
});
```

**In `Configure`:**

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

 ✅ **3. `AuthController` to Generate JWT**

```csharp
[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    [HttpGet("generate")]
    public IActionResult GetToken()
    {
        var token = GenerateJSONWebToken(1, "Admin");
        return Ok(new { token });
    }

    private string GenerateJSONWebToken(int userId, string userRole)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, userRole),
            new Claim("UserId", userId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "mySystem",
            audience: "myUsers",
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

✅ **4. `EmployeeController` with Authorization**

```csharp
[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin,POC")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(new { Message = "Authorized - You have access!" });
    }
}
```

✅ **5. Test with Postman**

🔸 Steps:

1. **GET `AuthController/generate`** → Copy the token.
2. **GET `EmployeeController`** with header:

```
Key: Authorization
Value: Bearer <token>
```

* ✅ Valid token & role → Status **200 OK**
* ❌ Invalid/expired/missing token → Status **401 Unauthorized**

✅ **6. Check JWT Expiration**

In `GenerateJSONWebToken`, update:

```csharp
expires: DateTime.Now.AddMinutes(2),
```

* Wait 2+ minutes, then send GET request.
* Result: **401 Unauthorized**.

---

✅ **7. Check Role-based Access Control**

### Case 1: `Authorize(Roles = "POC")`, token has `Admin`

* Result: **401 Unauthorized**

### Case 2: `Authorize(Roles = "Admin,POC")`, token has `Admin`

* Result: **200 OK**


