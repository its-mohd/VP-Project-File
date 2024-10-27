using System;
using System.Collections.Generic;
using System.Timers;

public class Product
{
    public string Name { 
        get; set;
    }
    public decimal Price { 
        get; set; 
    }
    public int Quantity { 
        get; set; 
    }

    public Product(string name, decimal price, int quantity = 1)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public decimal TotalPrice()
    {
        return Price * Quantity;
    }
}

public class ShoppingCart
{
    private Dictionary<string, Product> cart;
    private Dictionary<string, Dictionary<string, Product>> categories;
    private Dictionary<string, int> categoryProductCount;
    private const decimal SalesTax = 0.08m;
    private decimal discount = 0m;
    private System.Timers.Timer cartExpirationTimer;
    private const int ExpirationTimeInMinutes = 5;

    public ShoppingCart()
    {
        cart = new Dictionary<string, Product>();
        categories = new Dictionary<string, Dictionary<string, Product>>();
        categoryProductCount = new Dictionary<string, int>();
        InitializeCategories();
        InitializeCartExpiration();
    }

    private void InitializeCategories()
    {
        categories.Add("electronics", new Dictionary<string, Product>()
    {
        { "Laptop", new Product("Laptop", 950m) },
        { "Smartphone", new Product("Smartphone", 750m) },
        { "Headphones", new Product("Headphones", 150m) },
        { "Smartwatch", new Product("Smartwatch", 200m) },
        { "Camera", new Product("Camera", 600m) }
    });

        categories.Add("fashion", new Dictionary<string, Product>()
    {
        { "Sneakers", new Product("Sneakers", 90m) },
        { "Hoodie", new Product("Hoodie", 45m) },
        { "Jeans", new Product("Jeans", 55m) },
        { "Backpack", new Product("Backpack", 35m) },
        { "Cap", new Product("Cap", 18m) }
    });

        categories.Add("groceries", new Dictionary<string, Product>()
    {
        { "Almond Milk", new Product("Almond Milk", 3.5m) },
        { "Organic Bread", new Product("Organic Bread", 2.5m) },
        { "Free-Range Eggs", new Product("Free-Range Eggs", 6m) },
        { "Greek Yogurt", new Product("Greek Yogurt", 4m) },
        { "Honey", new Product("Honey", 8m) }
    });

        categories.Add("home appliances", new Dictionary<string, Product>()
    {
        { "Air Conditioner", new Product("Air Conditioner", 500m) },
        { "Washing Machine", new Product("Washing Machine", 700m) },
        { "Refrigerator", new Product("Refrigerator", 900m) },
        { "Dishwasher", new Product("Dishwasher", 400m) },
        { "Blender", new Product("Blender", 60m) }
    });

        categories.Add("books", new Dictionary<string, Product>()
    {
        { "Science Fiction", new Product("Science Fiction", 25m) },
        { "Mystery Novel", new Product("Mystery Novel", 18m) },
        { "Cookbook", new Product("Cookbook", 28m) },
        { "Poetry Collection", new Product("Poetry Collection", 15m) },
        { "Graphic Novel", new Product("Graphic Novel", 20m) }
    });

        categories.Add("sports", new Dictionary<string, Product>()
    {
        { "Dumbbells", new Product("Dumbbells", 35m) },
        { "Running Shoes", new Product("Running Shoes", 75m) },
        { "Treadmill", new Product("Treadmill", 300m) },
        { "Cycling Helmet", new Product("Cycling Helmet", 45m) },
        { "Resistance Bands", new Product("Resistance Bands", 15m) }
    });

        categories.Add("toys", new Dictionary<string, Product>()
    {
        { "Lego Set", new Product("Lego Set", 50m) },
        { "Action Figure", new Product("Action Figure", 18m) },
        { "Toy Car", new Product("Toy Car", 25m) },
        { "Puzzle", new Product("Puzzle", 12m) },
        { "Doll House", new Product("Doll House", 65m) }
    });

        categories.Add("automotive", new Dictionary<string, Product>()
    {
        { "Wiper Blades", new Product("Wiper Blades", 20m) },
        { "Car Wax", new Product("Car Wax", 15m) },
        { "GPS Navigator", new Product("GPS Navigator", 150m) },
        { "Tire Inflator", new Product("Tire Inflator", 45m) },
        { "Jump Starter", new Product("Jump Starter", 70m) }
    });

        categories.Add("beauty", new Dictionary<string, Product>()
    {
        { "Moisturizer", new Product("Moisturizer", 25m) },
        { "Hair Serum", new Product("Hair Serum", 15m) },
        { "Face Mask", new Product("Face Mask", 8m) },
        { "Makeup Brush Set", new Product("Makeup Brush Set", 40m) },
        { "Sunscreen", new Product("Sunscreen", 18m) }
    });

        categories.Add("stationery", new Dictionary<string, Product>()
    {
        { "Highlighter Set", new Product("Highlighter Set", 3m) },
        { "Stapler", new Product("Stapler", 4m) },
        { "Scissors", new Product("Scissors", 2.5m) },
        { "Glue Stick", new Product("Glue Stick", 1.2m) },
        { "Post-it Notes", new Product("Post-it Notes", 2m) }
    });

        categories.Add("health", new Dictionary<string, Product>()
    {
        { "Vitamins", new Product("Vitamins", 12m) },
        { "Protein Powder", new Product("Protein Powder", 35m) },
        { "Thermometer", new Product("Thermometer", 10m) },
        { "Blood Pressure Monitor", new Product("Blood Pressure Monitor", 45m) },
        { "Hand Sanitizer", new Product("Hand Sanitizer", 5m) }
    });

        categories.Add("furniture", new Dictionary<string, Product>()
    {
        { "Dining Table", new Product("Dining Table", 250m) },
        { "Sofa", new Product("Sofa", 500m) },
        { "Office Chair", new Product("Office Chair", 100m) },
        { "Bookshelf", new Product("Bookshelf", 80m) },
        { "Coffee Table", new Product("Coffee Table", 60m) }
    });

        categories.Add("jewelry", new Dictionary<string, Product>()
    {
        { "Ring", new Product("Ring", 200m) },
        { "Bracelet", new Product("Bracelet", 100m) },
        { "Necklace", new Product("Necklace", 150m) },
        { "Earrings", new Product("Earrings", 75m) },
        { "Watch", new Product("Watch", 300m) }
    });

        categories.Add("outdoor", new Dictionary<string, Product>()
    {
        { "Tent", new Product("Tent", 120m) },
        { "Camping Chair", new Product("Camping Chair", 30m) },
        { "Sleeping Bag", new Product("Sleeping Bag", 50m) },
        { "Portable Grill", new Product("Portable Grill", 40m) },
        { "Lantern", new Product("Lantern", 20m) }
    });

        categories.Add("garden", new Dictionary<string, Product>()
    {
        { "Flower Pot", new Product("Flower Pot", 5m) },
        { "Garden Hose", new Product("Garden Hose", 15m) },
        { "Shovel", new Product("Shovel", 10m) },
        { "Lawn Mower", new Product("Lawn Mower", 150m) },
        { "Watering Can", new Product("Watering Can", 8m) }
    });

        foreach (var category in categories.Keys)
        {
            categoryProductCount[category] = 0;
        }
    }

    private void InitializeCartExpiration()
    {
        cartExpirationTimer = new System.Timers.Timer(ExpirationTimeInMinutes * 60 * 1000);
        cartExpirationTimer.Elapsed += OnCartExpired;
        cartExpirationTimer.Start();
    }

    private void OnCartExpired(object sender, ElapsedEventArgs e)
    {
        cart.Clear();
        cartExpirationTimer.Stop();
        Console.WriteLine("\nYour cart has expired due to inactivity and has been cleared.");
    }

    public void ResetCartExpirationTimer()
    {
        cartExpirationTimer.Stop();
        cartExpirationTimer.Start();
    }

    public void AddToCart(string productName, int quantity)
    {
        ResetCartExpirationTimer();

        foreach (var category in categories)
        {
            if (category.Value.TryGetValue(productName, out var product))
            {
                if (cart.ContainsKey(product.Name))
                {
                    cart[product.Name].Quantity += quantity;
                }
                else
                {
                    cart[product.Name] = new Product(product.Name, product.Price, quantity);
                }
                Console.WriteLine($"{quantity}x {product.Name} added to the cart.");

                categoryProductCount[category.Key] += quantity;
                ApplyCategoryDiscount(category.Key);
                RecommendProducts(category.Key);
                return;
            }
        }
        Console.WriteLine("Product not found. Please check the available products.");
    }

    private void ApplyCategoryDiscount(string category)
    {
        int count = categoryProductCount[category];

        if (count > 2 && count < 4)
        {
            discount = 0.10m;
            Console.WriteLine("\nA 10% discount has been applied because you purchased more than 2 products from the category: " + category);
        }
        else if (count >= 4)
        {
            discount = 0.25m;
            Console.WriteLine("\nA 25% discount has been applied because you purchased 4 or more products from the category: " + category);
        }
        else
        {
            discount = 0m;
        }
    }

    private void RecommendProducts(string category)
    {
        Console.WriteLine($"\nYou may also like these products from {category}:");
        foreach (var product in categories[category].Values)
        {
            if (!cart.ContainsKey(product.Name))
            {
                Console.WriteLine($"- {product.Name}: ${product.Price}");
            }
        }
    }

    public void RemoveFromCart(string productName)
    {
        ResetCartExpirationTimer();

        if (cart.ContainsKey(productName))
        {
            Product removedProduct = cart[productName];
            cart.Remove(productName);

            foreach (var category in categories)
            {
                if (category.Value.ContainsKey(productName))
                {
                    categoryProductCount[category.Key] -= removedProduct.Quantity;
                    ApplyCategoryDiscount(category.Key);
                }
            }
            Console.WriteLine($"{productName} removed from the cart.");
        }
        else
        {
            Console.WriteLine("Product not found in the cart.");
        }
    }

    public void ViewCart()
    {
        ResetCartExpirationTimer();

        if (cart.Count == 0)
        {
            Console.WriteLine("The cart is empty.");
            return;
        }

        Console.WriteLine("\nYour Cart:");
        foreach (var item in cart.Values)
        {
            Console.WriteLine($"{item.Name} - ${item.Price} x {item.Quantity} = ${item.TotalPrice()}");
        }
    }

    public void ViewCategories()
    {
        Console.WriteLine("\nAvailable Categories and Products:");

        foreach (var category in categories)
        {
            Console.WriteLine($"\n{category.Key.ToUpper()}:");
            foreach (var product in category.Value.Values)
            {
                Console.WriteLine($"- {product.Name}: ${product.Price}");
            }
        }
    }

    public decimal GetTotalCost()
    {
        decimal total = 0m;

        foreach (var product in cart.Values)
        {
            total += product.TotalPrice();
        }

        total -= total * discount;
        total += total * SalesTax;

        return total;
    }

    public void Checkout()
    {
        if (cart.Count == 0)
        {
            Console.WriteLine("Cart is empty. Add items before checking out.");
            return;
        }

        decimal totalCost = GetTotalCost();
        Console.WriteLine($"\nYour total cost including sales tax is: ${totalCost}");
        cart.Clear();
        cartExpirationTimer.Stop();
        Console.WriteLine("Thank you for your purchase! The cart has been cleared.");
    }
}

public class Program
{
    private static string correctUsername = "mohd";
    private static string correctPassword = "mohd";

    private static bool Login()
    {
        Console.WriteLine("\t\t===================================================");
        Console.WriteLine("\t\t     Welcome to the Login Portal of Shopping Cart  ");
        Console.WriteLine("\t\t===================================================");

        Console.Write("\n\tEnter username: ");
        string username = Console.ReadLine();

        Console.Write("\n\tEnter password: ");
        string password = ReadPassword();

        if (username == correctUsername && password == correctPassword)
        {
            Console.WriteLine("\nLogin successful!");
            return true;
        }
        else
        {
            Console.WriteLine("\nIncorrect username or password. Please try again.");
            return false;
        }
    }

    private static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Remove(password.Length - 1);
                Console.Write("\b \b"); 
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }

        } while (keyInfo.Key != ConsoleKey.Enter);

        return password;
    }

    static void Main(string[] args)
    {
       
        while (!Login())
        {
         
            Console.WriteLine("Press any key to try again, or type 'exit' to quit.");
            if (Console.ReadLine()?.ToLower() == "exit")
            {
                Console.WriteLine("Goodbye!");
                return;
            }
        }
        ShoppingCart cart = new ShoppingCart();
        bool shopping = true;

        while (shopping)
        {
            Console.Clear();
            Console.WriteLine("\n\t\t=====================================");
            Console.WriteLine("\t\t     Welcome to the Shopping Cart    ");
            Console.WriteLine("\t\t=======================================");
            Console.WriteLine("\t\tPlease select an option from the menu");
            Console.WriteLine("\t\t-------------------------------------");
            Console.WriteLine("\t\t  1. View available products         ");
            Console.WriteLine("\t\t  2. Add a product to the cart       ");
            Console.WriteLine("\t\t  3. Remove Item from cart           ");
            Console.WriteLine("\t\t  4. View your cart                  ");
            Console.WriteLine("\t\t  5. Checkout and pay                ");
            Console.WriteLine("\t\t  6. Quit                            ");
            Console.WriteLine("\t\t-------------------------------------");
            Console.WriteLine("\n\t\tEnter your choice (1-5): ");
            Console.WriteLine("\t\t=====================================");

            string action = Console.ReadLine().ToLower();

            switch (action)
            {
                case "1":
                    cart.ViewCategories();
                    break;

                case "2":
                    Console.WriteLine("Enter product name:");
                    string productName = Console.ReadLine();
                    Console.WriteLine("Enter product quantity:");
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    cart.AddToCart(productName, quantity);
                    break;

                case "3":
                    Console.WriteLine("Enter product name to remove:");
                    string removeProductName = Console.ReadLine();
                    cart.RemoveFromCart(removeProductName);
                    break;

                case "4":
                    cart.ViewCart();
                    break;

                case "5":
                    cart.Checkout();
                    shopping = false;
                    break;

                case "6":
                    shopping = false;
                    Console.WriteLine("Thank you for visiting. Goodbye!");
                    continue;

                default:
                    Console.WriteLine("Invalid action. Please choose again.");
                    break;
            }

            
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }

}