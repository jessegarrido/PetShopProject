using Microsoft.Extensions.DependencyInjection;
using PetShop;
using System;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;

namespace PetShop
{
    internal class Program
    {
        
        public static IServiceProvider CreateServiceCollection() 
        {
            var services = new ServiceCollection();
            services.AddTransient<IProductLogic, ProductLogic>();
            services.AddTransient<IUILogic, UILogic>();
            return services.BuildServiceProvider();
        }
        static void Main(string[] args)
        {
            IServiceProvider services = CreateServiceCollection();
            //var uiLogic = new UILogic();
           // var productLogic = new ProductLogic();
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
                        Console.WriteLine("Enter the name of a product to retreive:");
                        string keyName = Console.ReadLine();
                        Product? foundProduct = uiLogic.RetrieveAProduct(keyName);
                        if (foundProduct != null)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(foundProduct));
                        } else
                        {
                            Console.WriteLine($"Product {keyName} was not found :(");
                        }
                        break;
                    case 3:
                        var totalCostOfInventory = productLogic.GetTotalPriceOfInventory();
                        Console.WriteLine($"Total cost of inventory in stock: ${totalCostOfInventory}"); 
                        break;
                    case 7:
                        var inStock = productLogic.GetOnlyInStockProducts();
                        
                        foreach (var productName in inStock)
                        {
                            Console.WriteLine(productName);
                        }
                        break;
                    case 8:
                        
                            var allProducts = productLogic.GetAllProducts();
                            foreach (var product in allProducts) {
                                Console.WriteLine(product.Name);
                            }
                        break;
                         }
            } while (selection != 0);

        }
    }

}
