using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace PetShop

{
    interface IUILogic
    {
        public void ShowMainMenu();
        public int GetValidUserSelection();
        public int GetValidUserSelection(List<int> usersValidOptions);
        public Product RetrieveAProduct(string name, ProductLogic productLogic);
    }
    public class UILogic : IUILogic
    {
        private List<int> _validMainMenuOptions { get; set; }
        public UILogic()
        {
            _validMainMenuOptions = new List<int>(new int[] { 1, 2, 3, 7, 8 });
        }
        public void ShowMainMenu()
        {
            Console.WriteLine("Press 1 to add a product");
            Console.WriteLine("Press 2 to retreive a product");
            Console.WriteLine("Press 3 to view total cost of inventory in stock");
            Console.WriteLine("Press 7 to view only products in stock");
            Console.WriteLine("Press 8 to view all products");
            Console.WriteLine("Type 'exit' to quit");
        }
        public int GetValidUserSelection()
        {
            int? validSelection = null;
            string input;
            do
            {
                input = Console.ReadLine();
                if (input.ToLower() == "exit") { return 0; }
                int.TryParse(input, out int userVal);
                validSelection = userVal;
            } while (!_validMainMenuOptions.Contains(validSelection ?? -1));
            return validSelection ?? -1;
        }
        public int GetValidUserSelection(List<int> userProvidedValidOptions)
        {
            string input;
            int? validSelection = null;
            do
            {
                input = Console.ReadLine();
                if (input.ToLower() == "exit") { return 0; }
                int.TryParse(input, out int userVal);
                validSelection = userVal;
            } while (!userProvidedValidOptions.Contains(validSelection ?? -1));
            return validSelection ?? -1;
        }
        public Product? RetrieveAProduct(string name, ProductLogic productLogic)
        {
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
