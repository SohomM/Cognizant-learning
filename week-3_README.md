**Lab 1: Understanding ORM with a Retail Inventory System**

1. **What is ORM?**

**ORM (Object-Relational Mapping)** is a programming technique that allows developers to interact with a relational database using object-oriented code, instead of SQL queries.

How ORM maps C# classes to database tables:
* **Each C# class = one database table**
* **Each property = one table column**
* **Each object instance = one row in the table**
* ORM frameworks handle this mapping automatically using **metadata, attributes, or conventions**.

Benefits of using ORM:
* **Productivity**: No need to write raw SQL queries repeatedly.
* **Maintainability**: Cleaner code with separation of concerns.
* **Abstraction**: Developers focus on business logic; database details are abstracted.
* **Portability**: Easy to switch databases with minimal code changes.

2️.  **EF Core vs EF Framework**

| Feature                | EF Core                                | EF Framework (EF6)                    |
| ---------------------- | -------------------------------------- | ------------------------------------- |
| **Platform**           | Cross-platform (Windows, Linux, macOS) | Windows-only                          |
| **Performance**        | Lightweight and faster                 | Heavier and slower                    |
| **Modern Features**    | Supports LINQ, async, compiled queries | Limited support                       |
| **Development Status** | Actively developed, latest features    | Older, limited new features           |
| **Migration Support**  | Better migrations, CLI support         | GUI-based migrations in Visual Studio |

>  **EF Core** is recommended for **new applications** due to its flexibility, performance, and cross-platform nature.

3️. **EF Core 8.0 Features**

EF Core 8 introduces several enhancements:
* **🧩 JSON Column Mapping**: Store and query complex types as JSON within a single database column.
* **🚀 Compiled Models**: Improves startup and runtime performance by pre-compiling the EF model.
* **🎯 Interceptors**: Customize and monitor DB operations (e.g., logging, caching).
* **📦 Bulk Operations**: Improved batch update/insert/delete for performance.

4️. **Create a .NET Console Application**

Use the .NET CLI to scaffold a new console app:

```bash
dotnet new console -n RetailInventory
cd RetailInventory
```

This creates a folder named `RetailInventory` and adds the necessary project files.

5️. **Install EF Core Packages**

Use the following commands to install required NuGet packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

* `Microsoft.EntityFrameworkCore.SqlServer`: Enables EF Core to work with SQL Server databases.
* `Microsoft.EntityFrameworkCore.Design`: Provides tools for scaffolding, migrations, etc.

Here is the direct and complete answer to **Lab 2: Setting Up the Database Context for a Retail Store**, exactly as per assignment requirements:

 
 **Lab 2: Setting Up the Database Context for a Retail Store**


1️. **Create Models**

```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
```

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
```

2️. **Create AppDbContext**

```csharp
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your_Connection_String_Here");
    }
}
```

3️. **Add Connection String in appsettings.json**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your_Connection_String_Here"
  }
}
```


 **Lab3:  EF Core CLI – Lab 3: Creating and Applying Migrations**

1. Install EF Core CLI

Install the EF Core command-line tool globally:

```bash
dotnet tool install --global dotnet-ef
```

This installs the `dotnet ef` command, used to create and apply migrations.

2. Create Initial Migration

Run this command inside the project folder:

```bash
dotnet ef migrations add InitialCreate
```

This command:

* Scans the `AppDbContext` and model classes (`Product`, `Category`)
* Generates a `Migrations/` folder containing the C# migration code

**Expected Output:**

```
Build succeeded.
Done. To undo this action, use: 'dotnet ef migrations remove'
```

3. Apply Migration to Create the Database

Apply the pending migration to the database using:

```bash
dotnet ef database update
```

This command:

* Creates the database specified in the connection string (`RetailInventoryDb`)
* Creates the tables `Products` and `Categories`
* Adds a special table `__EFMigrationsHistory` to track applied migrations

4. Verify Database in SQL Server

Open **SQL Server Management Studio (SSMS)** or **Azure Data Studio**, then:

* Connect to the server (e.g., `localhost`)
* Open the `RetailInventoryDb` database
* Expand **Tables**

**You should see:**

* `dbo.Products`
* `dbo.Categories`
* `dbo.__EFMigrationsHistory`

These tables are generated by EF Core based on the C# classes and configuration in `AppDbContext`.


## Folder Structure (after migration)

```
RetailInventory/
├── Program.cs
├── Models/
│   ├── Product.cs
│   └── Category.cs
├── Data/
│   └── AppDbContext.cs
├── Migrations/
│   ├── [timestamp]_InitialCreate.cs
│   └── ... (Designer and Snapshot files)
```

