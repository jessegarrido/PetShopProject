using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace PetShop

{
    interface IUILogic
    {
        public List<int> ShowMainMenu();
        //public int GetValidUserSelection();
        public int GetValidUserSelection(List<int> usersValidOptions);
        public Product RetrieveAProduct(string name);
    }
    public class UILogic : IUILogic
    {
        public UILogic()
        {
        }
        public List<int> ShowMainMenu()
        {
            Console.WriteLine("Press 1 to add a product");
            Console.WriteLine("Press 2 to retreive a product");
            Console.WriteLine("Press 3 to view total cost of inventory in stock");
            Console.WriteLine("Press 7 to view only products in stock");
            Console.WriteLine("Press 8 to view all products");
            Console.WriteLine("Type 'exit' to quit");
            var validMainMenuOptions = new List<int>(new int[] { 1, 2, 3, 7, 8 });
            return validMainMenuOptions;
        }

        public int GetValidUserSelection(List<int> validOptions)
        {
            string input;
            int? validSelection = null;
            do
            {
                input = Console.ReadLine();
                if (input.ToLower() == "exit") { return 0; }
                int.TryParse(input, out int userVal);
                validSelection = userVal;
            } while (!validOptions.Contains(validSelection ?? -1));
            return validSelection ?? -1;
        }
        public Product? RetrieveAProduct(string name)
        {
            var productLogic = new ProductLogic();
            var allDogLeash = productLogic.GetAllProducts().Where(x => x.GetType() == typeof(DogLeash));
            var allCatFood = productLogic.GetAllProducts().Where(x => x.GetType() == typeof(CatFood));
            if (allDogLeash.Count(p => p.Name == name) > 0)
            {
                return productLogic.GetDogLeashByName(name) as Product;
            } else if (allCatFood.Count(p => p.Name == name) > 0)
            {
                return productLogic.GetCatFoodByName(name) as Product;
            } else
            {
                return null;
            }
        }   
    }
}
