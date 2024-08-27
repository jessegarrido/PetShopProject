using PetShop;
using System;
using System.Text.Json;
using System.Xml.Linq;

namespace PetShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var uiLogic = new UILogic();
            var productLogic = new ProductLogic();
            int? selection = null;
            do
            {
                uiLogic.ShowMainMenu();
                selection = uiLogic.GetValidUserSelection();  
                switch (selection) {
                    case 1:
                        productLogic.DefineNewProduct(uiLogic);
                        break;
                    case 2:
                        Console.WriteLine("Enter the name of a product to retreive:");
                        string keyName = Console.ReadLine();
                        Product? foundProduct = uiLogic.RetrieveAProduct(keyName, productLogic);
                        if (foundProduct != null)
                        {
                            Console.WriteLine(JsonSerializer.Serialize(foundProduct));
                        } else
                        {
                            Console.WriteLine($"Product {keyName} was not found :(");
                        }
                        /*
                        Console.WriteLine(JsonSerializer.Serialize(foundProduct));/*
                        //var allDogLeash = productLogic.GetAllProducts().Where(x => x.GetType() == typeof(DogLeash));
                       // var allCatFood = productLogic.GetAllProducts().Where(x => x.GetType() == typeof(CatFood));
                        
                        string keyName = Console.ReadLine();
                        if (allDogLeash.Where(p => p.Name == keyName).Count() > 0)
                        {
                            var dispLeash = productLogic.GetDogLeashByName(keyName);
                            Console.WriteLine(JsonSerializer.Serialize(dispLeash));
                        }
                        else if (allCatFood.Where(p => p.Name == keyName).Count() > 0)
                        {
                            var dispFood = productLogic.GetCatFoodByName(keyName);
                            Console.WriteLine(JsonSerializer.Serialize(dispFood));
                        }
                            else
                            {
                            Console.WriteLine($"{keyName} was not found :( ");
                            }
                        */
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
