using System;

namespace ECommerceSearch
{
    // Step 2: Product class
    public class Product
    {
        public int ProductId;
        public string ProductName;
        public string Category;

        public Product(int id, string name, string category)
        {
            ProductId = id;
            ProductName = name;
            Category = category;
        }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}, Category: {Category}";
        }
    }

    class Program
    {
        // Step 3: Linear Search
        static Product LinearSearch(Product[] products, string name)
        {
            foreach (var product in products)
            {
                if (product.ProductName == name)
                    return product;
            }
            return null;
        }

        // Step 3: Binary Search (Assumes products are sorted by name)
        static Product BinarySearch(Product[] products, string name)
        {
            int left = 0, right = products.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int comparison = string.Compare(products[mid].ProductName, name, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                    return products[mid];
                else if (comparison < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return null;
        }

        static void Main(string[] args)
        {
            // Sample product list (unsorted)
            Product[] products = {
                new Product(1, "Laptop", "Electronics"),
                new Product(2, "T-Shirt", "Clothing"),
                new Product(3, "Book", "Education"),
                new Product(4, "Shoes", "Footwear"),
                new Product(5, "Smartphone", "Electronics")
            };

            // Use Linear Search
            Console.WriteLine("Linear Search: Looking for 'Book'");
            var result1 = LinearSearch(products, "Book");
            Console.WriteLine(result1 != null ? result1.ToString() : "Product not found");

            // Sort array for Binary Search
            Array.Sort(products, (p1, p2) => p1.ProductName.CompareTo(p2.ProductName));

            // Use Binary Search
            Console.WriteLine("\nBinary Search: Looking for 'Book'");
            var result2 = BinarySearch(products, "Book");
            Console.WriteLine(result2 != null ? result2.ToString() : "Product not found");

            // Step 4: Analysis
            Console.WriteLine("\n--- Search Analysis ---");
            Console.WriteLine("Linear Search Time Complexity: O(n)");
            Console.WriteLine("Binary Search Time Complexity: O(log n)");
            Console.WriteLine("Binary Search is faster for large, sorted datasets.");
        }
    }
}

