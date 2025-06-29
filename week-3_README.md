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


**Lab 4: Inserting Initial Data**

1. Inserting Data in Program.cs

Updating `Program.cs` file in the console application project to include the following code:

```csharp
using System.Threading.Tasks;
using RetailInventory.Models;
using RetailInventory.Data;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        var electronics = new Category { Name = "Electronics" };
        var groceries = new Category { Name = "Groceries" };

        await context.Categories.AddRangeAsync(electronics, groceries);

        var product1 = new Product { Name = "Laptop", Price = 75000, Category = electronics };
        var product2 = new Product { Name = "Rice Bag", Price = 1200, Category = groceries };

        await context.Products.AddRangeAsync(product1, product2);
        await context.SaveChangesAsync();

        Console.WriteLine("Data inserted successfully.");
    }
}
```


2. Run the Application

Execute the project using the following command:

```bash
dotnet run
```

**Expected Console Output:**

```
Data inserted successfully.
```

3. Verify in SQL Server

Open **SQL Server Management Studio (SSMS)** or **Azure Data Studio**, then:

* Connect to the `RetailInventoryDb` database
* Run the following SQL queries:

```sql
SELECT * FROM Categories;
SELECT * FROM Products;
```

**Expected Data:**

Categories Table

| Id | Name        |
| -- | ----------- |
| 1  | Electronics |
| 2  | Groceries   |

Products Table

| Id | Name     | Price | CategoryId |
| -- | -------- | ----- | ---------- |
| 1  | Laptop   | 75000 | 1          |
| 2  | Rice Bag | 1200  | 2          |



## Folder and Project Structure

```
RetailInventory/
├── Program.cs
├── Models/
│   ├── Product.cs
│   └── Category.cs
├── Data/
│   └── AppDbContext.cs
├── appsettings.json
├── Migrations/
│   ├── [timestamp]_InitialCreate.cs
│   └── ... (EF migration files)
```


# Report: EF Core CLI – Lab 5: Retrieving Data from the Database


1. Retrieve All Products

Add the following code to `Program.cs` or a new method within your application:

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;
using RetailInventory.Data;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        // 1. Retrieve all products
        var products = await context.Products.ToListAsync();
        foreach (var p in products)
            Console.WriteLine($"{p.Name} - ₹{p.Price}");

        // 2. Find by ID
        var product = await context.Products.FindAsync(1);
        Console.WriteLine($"Found: {product?.Name}");

        // 3. FirstOrDefault with condition
        var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
        Console.WriteLine($"Expensive: {expensive?.Name}");
    }
}
```

Ensure your `Product` and `AppDbContext` classes are correctly set up and referenced.

2. Run the Application

Use the .NET CLI to run the console application:

```bash
dotnet run
```

**Expected Output:**

```
Laptop - ₹75000
Rice Bag - ₹1200
Found: Laptop
Expensive: Laptop
```

3. Explanation of Methods

| Method                  | Description                                                             |
| ----------------------- | ----------------------------------------------------------------------- |
| `ToListAsync()`         | Retrieves all entries from the table as a list                          |
| `FindAsync(id)`         | Searches for a row by its primary key (faster when tracking is enabled) |
| `FirstOrDefaultAsync()` | Retrieves the first matching record or null if none match the condition |

---

## Folder and Project Structure

```
RetailInventory/
├── Program.cs
├── Models/
│   ├── Product.cs
│   └── Category.cs
├── Data/
│   └── AppDbContext.cs
├── appsettings.json
├── Migrations/
│   ├── [timestamp]_InitialCreate.cs
│   └── ...
```
