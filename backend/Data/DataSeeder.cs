﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using StockManagement.Models;

namespace StockManagement.Data
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Define role names
                var roles = new List<string> { "Admin", "StockManager" };

                // Create roles if they do not exist
                foreach (var roleName in roles)
                {
                    if (!roleManager.RoleExistsAsync(roleName).Result)
                    {
                        var role = new IdentityRole(roleName);
                        roleManager.CreateAsync(role).Wait();
                    }
                }

                // Create the admin user
                string adminEmail = "admin@gmail.com";
                var adminUser = userManager.FindByEmailAsync(adminEmail).Result;
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = adminEmail
                    };

                    var result = userManager.CreateAsync(adminUser, "Admin2003*").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }

                // Create the sale manager user
                string saleManagerEmail = "stockmanager@gmail.com";
                var saleManagerUser = userManager.FindByEmailAsync(saleManagerEmail).Result;
                if (saleManagerUser == null)
                {
                    saleManagerUser = new IdentityUser
                    {
                        UserName = "stockmanager",
                        Email = saleManagerEmail
                    };

                    var result = userManager.CreateAsync(saleManagerUser, "StockManager2003*").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(saleManagerUser, "StockManager").Wait();
                    }
                }
                
                
                
                
                
                
                
                
                
                
                // Look for any existing data.
                
                
                
                if (context.Clients.Any() || context.Categories.Any() || context.Manufacturers.Any() || context.Suppliers.Any() || context.Products.Any() || context.Warehouses.Any())
                {
                    return;   // DB has been seeded
                }

                // Add initial data
                context.Warehouses.AddRange(
                    new Warehouse
                    {
                        Name = "Main Warehouse", Location = "123 Main St, City, Country",
                        Locations = GenerateLocations("Main Warehouse") // Generate 40 locations
                        
                    }
                );
                
                context.Clients.AddRange(
                    new Client { LastName = "Doe", FirstName = "John", Email = "john.doe@example.com", Address = "123 Main St", PhoneNumber = "123-456-7890", RegistrationDate = DateTime.Now },
                    new Client { LastName = "Smith", FirstName = "Jane", Email = "jane.smith@example.com", Address = "456 Elm St", PhoneNumber = "987-654-3210", RegistrationDate = DateTime.Now },
                    new Client { LastName = "Brown", FirstName = "Charlie", Email = "charlie.brown@example.com", Address = "789 Oak St", PhoneNumber = "555-123-4567", RegistrationDate = DateTime.Now },
                    new Client { LastName = "Johnson", FirstName = "Emily", Email = "emily.johnson@example.com", Address = "321 Pine St", PhoneNumber = "555-987-6543", RegistrationDate = DateTime.Now },
                    new Client { LastName = "Williams", FirstName = "Michael", Email = "michael.williams@example.com", Address = "654 Cedar St", PhoneNumber = "555-654-3210", RegistrationDate = DateTime.Now }
                );

                context.Categories.AddRange(
                    new Category { Name = "Clothing", Description = "Apparel and garments" },
                    new Category { Name = "Electronics", Description = "Electronic devices and gadgets" },
                    new Category { Name = "Food", Description = "Edible items and groceries" }
                );

                context.Manufacturers.AddRange(
                    new Manufacturer { Name = "Manufacturer A", Address = "789 Maple St", Email = "contact@manufacturerA.com", Phone = "555-123-4567" },
                    new Manufacturer { Name = "Manufacturer B", Address = "321 Oak St", Email = "contact@manufacturerB.com", Phone = "555-987-6543" },
                    new Manufacturer { Name = "Manufacturer C", Address = "654 Pine St", Email = "contact@manufacturerC.com", Phone = "555-654-3210" },
                    new Manufacturer { Name = "Manufacturer D", Address = "987 Birch St", Email = "contact@manufacturerD.com", Phone = "555-321-0987" },
                    new Manufacturer { Name = "Manufacturer E", Address = "123 Cedar St", Email = "contact@manufacturerE.com", Phone = "555-789-0123" }
                );

                context.Suppliers.AddRange(
                    new Supplier { Name = "Supplier A", Email = "supplierA@example.com", Address = "654 Pine St", Phone = "555-654-3210", RegistrationDate = DateTime.Now },
                    new Supplier { Name = "Supplier B", Email = "supplierB@example.com", Address = "987 Birch St", Phone = "555-321-0987", RegistrationDate = DateTime.Now },
                    new Supplier { Name = "Supplier C", Email = "supplierC@example.com", Address = "123 Cedar St", Phone = "555-789-0123", RegistrationDate = DateTime.Now },
                    new Supplier { Name = "Supplier D", Email = "supplierD@example.com", Address = "456 Elm St", Phone = "555-456-7890", RegistrationDate = DateTime.Now },
                    new Supplier { Name = "Supplier E", Email = "supplierE@example.com", Address = "789 Oak St", Phone = "555-123-4567", RegistrationDate = DateTime.Now }
                );

                context.Products.AddRange(
                    new ClothingProduct { Name = "T-Shirt", Price = 19.99m, StockQuantity = 0, CategoryId = 1, ManufacturerId = 1, FabricType = "Cotton", Size = ClothingProductStatus.M },
                    new ElectronicProduct { Name = "Smartphone", Price = 299.99m, StockQuantity = 0, CategoryId = 2, ManufacturerId = 2, WarrantyYears = 2, EnergyClass = "A+" },
                    new FoodProduct { Name = "Apple", Price = 0.99m, StockQuantity = 0, CategoryId = 3, ManufacturerId = 1, StorageTemperature = 4 },
                    new ClothingProduct { Name = "Jeans", Price = 49.99m, StockQuantity = 0, CategoryId = 1, ManufacturerId = 2, FabricType = "Denim", Size = ClothingProductStatus.L },
                    new ElectronicProduct { Name = "Laptop", Price = 999.99m, StockQuantity = 0, CategoryId = 2, ManufacturerId = 3, WarrantyYears = 3, EnergyClass = "A" },
                    new FoodProduct { Name = "Banana", Price = 0.59m, StockQuantity = 0, CategoryId = 3, ManufacturerId = 2, StorageTemperature = 5 },
                    new ClothingProduct { Name = "Jacket", Price = 79.99m, StockQuantity = 0, CategoryId = 1, ManufacturerId = 3, FabricType = "Leather", Size = ClothingProductStatus.S },
                    new ElectronicProduct { Name = "Headphones", Price = 49.99m, StockQuantity = 0, CategoryId = 2, ManufacturerId = 4, WarrantyYears = 1, EnergyClass = "B" },
                    new FoodProduct { Name = "Orange", Price = 0.79m, StockQuantity = 0, CategoryId = 3, ManufacturerId = 3, StorageTemperature = 6 }
                );

                context.SaveChanges();
            }
        }
        
        private static List<Location> GenerateLocations(string warehouseName)
        {
            var locations = new List<Location>();

            // Add specific locations for suppliers, buyers, and expired products
            locations.Add(new Location { Name = $"{warehouseName} - Supplier Area" });
            locations.Add(new Location { Name = $"{warehouseName} - Buyer Area" });
            locations.Add(new Location { Name = $"{warehouseName} - Expired Products Area" });

            // Add 40 additional locations
            for (int i = 1; i <= 10; i++)
            {
                locations.Add(new Location { Name = $"{warehouseName} - Location A-{i}" ,  isEmpty = true});
            }
            
            for (int i = 1; i <= 10; i++)
            {
                locations.Add(new Location { Name = $"{warehouseName} - Location B-{i}" ,  isEmpty = true});
            }
         
            for (int i = 1; i <= 10; i++)
            {
                locations.Add(new Location { Name = $"{warehouseName} - Location C-{i}" ,  isEmpty = true});
            }
            
            for (int i = 1; i <= 10; i++)
            {
                locations.Add(new Location { Name = $"{warehouseName} - Location D-{i}",  isEmpty = true });
            }
            
            return locations;
        }
    }
}