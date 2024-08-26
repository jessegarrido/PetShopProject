using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace PetShop

{
    interface IUILogic
    {
        public void ShowMainMenu();
        public int GetValidUserUserSelection();
        public int GetValidUserUserSelection(List<int> usersValidOptions);

    }
    public class UILogic : IUILogic
    {
        private List<int> _validOptions { get; set; }
        public UILogic()
        {
            _validOptions = new List<int>(new int[] { 1, 2, 3, 7, 8 });
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
        public int GetValidUserUserSelection()
        {
            int? validSelection = null;
            string input;
            do
            {
                input = Console.ReadLine();
                if (input.ToLower() == "exit") { return 0; }
                int.TryParse(input, out int userVal);
                validSelection = userVal;
            } while (!_validOptions.Contains(validSelection ?? -1));
            return validSelection ?? -1;
        }
        public int GetValidUserUserSelection(List<int> userProvidedValidOptions)
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

    }
}
