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
            Console.WriteLine("Press 1 to add a product");
            Console.WriteLine("Press 2 to retreive a product");
            Console.WriteLine("Press 7 to view only products in stock");
            Console.WriteLine("Press 8 to view all products");
            Console.WriteLine("Type 'exit' to quit");
            string userInput = Console.ReadLine();
            var productLogic = new ProductLogic();
            while (userInput.ToLower() != "exit")
            {
                switch (int.Parse(userInput)) {
                    case 1:
                        CatFood catFood = new CatFood();
                        DogLeash dogLeash = new DogLeash();

                        int? validInt = null;
                        do
                        {
                            Console.WriteLine("Press 1 to add Cat Food or 2 to Add a Dog Leash");
                            userInput = Console.ReadLine();
                            if (int.TryParse(userInput, out int userVal))
                            {
                                validInt = userVal;
                            }
                        } while (validInt == null || validInt < 1 || validInt > 2);
                        switch (validInt ?? 1) {
                            case 1:

                                Console.Write("Add product name: ");
                                catFood.Name = Console.ReadLine();
                                Console.Write("Add product price ");
                                catFood.Price = decimal.Parse(Console.ReadLine());
                                Console.Write("Add product quantity: ");
                                catFood.Quantity = int.Parse(Console.ReadLine());
                                Console.Write("Add product description: ");
                                catFood.Description = Console.ReadLine();
                                Console.Write("Add product weight in pounds: ");
                                catFood.WeightPounds = int.Parse(Console.ReadLine());
                                Console.Write("For kittens? ");
                                catFood.KittenFood = bool.Parse(Console.ReadLine());
                                productLogic.AddProduct(catFood);
                                break;
                            case 2:
                                Console.Write("Add product name: ");
                                dogLeash.Name = Console.ReadLine();
                                Console.Write("Add product price ");
                                dogLeash.Price = decimal.Parse(Console.ReadLine());
                                Console.Write("Add product quantity: ");
                                dogLeash.Quantity = int.Parse(Console.ReadLine());
                                Console.Write("Add product description: ");
                                dogLeash.Description = Console.ReadLine();
                                Console.Write("Add product length in inches: ");
                                dogLeash.LengthInches = int.Parse(Console.ReadLine());
                                Console.Write("Add product material: ");
                                dogLeash.Material = Console.ReadLine();
                                productLogic.AddProduct(dogLeash);
                                break;
                        }
                        /*
                        catFood.Price = 6.99M;
                        catFood.Quantity = 4;
                        catFood.Description = "It's Tasty riffic, meow!";
                        catFood.WeightPounds = 7;
                        catFood.KittenFood = true;
                        Console.WriteLine($"{catFood.Name} was added!");
                        //string jsonOut = JsonSerializer.Serialize(catFood);
                        //Console.WriteLine(jsonOut);
                        dogLeash.Name = "Leashomatic";
                        dogLeash.Price = 14.99M;
                        dogLeash.Quantity = 1;
                        dogLeash.Description = "It's a leash but automatic";
                        dogLeash.LengthInches = 27;
                        dogLeash.Material = "nylon";
                        Console.WriteLine($"{dogLeash.Name} was added!");
                        productLogic.AddProduct(catFood);
                        productLogic.AddProduct(dogLeash);
                        */
                        break;
                    case 2:
                        var allDogLeash = productLogic.GetAllProducts().Where(x => x.GetType() == typeof(DogLeash));
                        var allCatFood = productLogic.GetAllProducts().Where(x => x.GetType() == typeof(CatFood));
                        Console.WriteLine("Enter the name of a product to retreive:");
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
                Console.WriteLine("Press 1 to add a product");
                Console.WriteLine("Press 2 to retreive a product");
                Console.WriteLine("Press 7 to view only products in stock");
                Console.WriteLine("Press 8 to view all products");
                Console.WriteLine("Type 'exit' to quit");
                userInput = Console.ReadLine();
            }
            
        }
    }

}
