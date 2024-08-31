using PetShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace PetShop
{
    static class ListExtensions
    {
        public static IList<T> InStock<T>(this IList<T> list) where T : Product
        {
            return list.Where(x => x.Quantity > 0).ToList();
        }
    }
    public interface IProductLogic {
        public void AddProductToDictionary(Product product);
        public List<Product> GetAllProducts();
        public DogLeash GetDogLeashByName(string name);
        public CatFood GetCatFoodByName(string name);
        public List<string> GetOnlyInStockProducts();
        public decimal GetTotalPriceOfInventory();
        public void DefineNewProduct(UILogic uilogic);
    }

    public class ProductLogic : IProductLogic
    {
        private List<Product> _products { get; set; }
        private Dictionary<string, DogLeash?> _dogLeash { get; set; }
        private Dictionary<string, CatFood?> _catFood { get; set; }
        public ProductLogic() {
            _dogLeash = new Dictionary<string, DogLeash?>();
            _catFood = new Dictionary<string, CatFood?>();
            _products = new List<Product>();
            _products.Add(new CatFood()
            {
                Name = "Catteriffic",
                Price = 6.99M,
                Quantity = 4,
                Description = "It's Tasty riffic, meow!",
                WeightPounds = 7,
                KittenFood = true
            });
            _catFood.Add(_products[0].Name, _products[0] as CatFood);
            _products.Add(new DogLeash()
            {
                Name = "Leashomatic",
                Price = 4.55M,
                Quantity = 6,
                Description = "It's a leash but automatic",
                LengthInches = 27,
                Material = "nylon"
            });
            _dogLeash.Add(_products[1].Name, _products[1] as DogLeash);
            _products.Add(new DogLeash()
            {
                Name = "Leash Unleashed",
                Price = 99.55M,
                Quantity = 0,
                Description = "Don't let a short leash hold you back",
                LengthInches = 4,
                Material = "titanium"
            });
            _dogLeash.Add(_products[2].Name, _products[2] as DogLeash);
        }

        public void AddProductToDictionary(Product product)
        {
            _products.Add(product);
            if (product is DogLeash)
            {
                _dogLeash.Add(product.Name, product as DogLeash);
            }
            else if (product is CatFood)
            {
                _catFood.Add(product.Name, product as CatFood);
            }

        }
        public List<Product> GetAllProducts()
        {
            return _products;
        }
        public List<string> GetOnlyInStockProducts()
        {
            return _products.InStock().Select(x => x.Name.ToString()).ToList();
        }

        public DogLeash GetDogLeashByName(string name)
        {
            try
            {
                return _dogLeash[name];
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public CatFood GetCatFoodByName(string name)
        {
            try
            {
                return _catFood[name];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public decimal GetTotalPriceOfInventory(){
            return _products.InStock().Sum(x=>x.Price*x.Quantity) ?? 0;
        }
        public void DefineNewProduct(UILogic uiLogic)
        {
            var validNewTypes = new List<int> { 1, 2 };
            Console.WriteLine("Press 1 to add Cat Food or 2 to Add a Dog Leash");
            int typeSelection = uiLogic.GetValidUserSelection(validNewTypes);
            Product newProduct = new Product();
            Console.Write("Add product name: ");
            newProduct.Name = Console.ReadLine();
            Console.Write("Add product price ");
            newProduct.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Add product quantity: ");
            newProduct.Quantity = int.Parse(Console.ReadLine());
            Console.Write("Add product description: ");
            newProduct.Description = Console.ReadLine();

            switch (typeSelection)
            {
                case 1:
                    var newCatFood = new CatFood();
                    newCatFood.Name = newProduct.Name;
                    newCatFood.Price = newProduct.Price;
                    newCatFood.Quantity = newProduct.Quantity;
                    newCatFood.Description = newProduct.Description;
                    Console.Write("Add product weight in pounds: ");
                    newCatFood.WeightPounds = int.Parse(Console.ReadLine());
                    Console.Write("For kittens? ");
                    newCatFood.KittenFood = bool.Parse(Console.ReadLine());
                    this.AddProductToDictionary(newCatFood);
                    break;
                case 2:
                    var newDogLeash = new DogLeash();
                    newDogLeash.Name = newProduct.Name;
                    newDogLeash.Price = newProduct.Price;
                    newDogLeash.Quantity = newProduct.Quantity;
                    newDogLeash.Description = newProduct.Description;
                    Console.Write("Add product length in inches: ");
                    newDogLeash.LengthInches = int.Parse(Console.ReadLine());
                    Console.Write("Add product material: ");
                    newDogLeash.Material = Console.ReadLine();
                    this.AddProductToDictionary(newDogLeash);
                    break;
                default:
                    break;
            }
        }

    }
}