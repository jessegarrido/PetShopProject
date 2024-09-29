using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using PetStore;
using System.Reflection;
using System;

namespace PetShop
{
    internal class Program
    {
        public static IServiceProvider CreateServiceCollection() 
        {
            var services = new ServiceCollection();
            services.AddTransient<IProductLogic, ProductLogic>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUILogic, UILogic>();
            return services.BuildServiceProvider();
        }
        static void Main(string[] args)
        {
            IServiceProvider services = CreateServiceCollection();
            var petstoredata = services.GetService<IProductRepository>();
            var productLogic = services.GetService<IProductLogic>();
            var uiLogic = services.GetService<IUILogic>();
            int ? selection = null;
            do
            {
                var validOptions = uiLogic.ShowMainMenu();
                selection = uiLogic.GetValidUserSelection(validOptions);  
                switch (selection) {
                    case 1:
                        productLogic.DefineNewProduct((UILogic)uiLogic);
                        break;
                    case 2:
                        Console.WriteLine("Enter the id of a product to retreive:");
                        string id = Console.ReadLine();
                        var foundProduct = productLogic.GetProductById(id);
                        if (foundProduct != null)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(foundProduct));
                        } else
                        {
                            Console.WriteLine($"ProductId {id} was not found :(");
                        }
                        break;
                    //case 3:
                    //    var totalCostOfInventory = productLogic.GetTotalPriceOfInventory();
                    //    Console.WriteLine($"Total cost of inventory in stock: ${totalCostOfInventory}"); 
                    //    break;
                    //case 7:
                    //    var inStock = productLogic.GetOnlyInStockProducts();
                    //    foreach (var productName in inStock)
                    //    {
                    //        Console.WriteLine(productName);
                    //    }
                    //    break;
                    case 8:
                            var allProducts = productLogic.GetAllProducts();
                            foreach (var product in allProducts) {
                            //    Console.WriteLine(product.Name);
                            //Product product = new Product();
                            PropertyInfo[] properties = typeof(Product).GetProperties();
                            foreach (PropertyInfo property in properties)
                            {
                                object propertyValue = property.GetValue(product);
                                Console.WriteLine($"{property.Name} : {propertyValue}");
                            }
                        }
                        break;
                         }
            } while (selection != 0);

        }
    }

}
