using PetShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Nodes;
using PetShop.Validators;
using FluentValidation;
using Petshop;

namespace PetShop
{
    public interface IProductLogic {
        public void AddProductToDictionary(Product product);
        public List<Product> GetAllProducts();
        public T GetProductByName<T>(string name) where T : Product;
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

        public T GetProductByName<T>(string name) where T : Product
        {
            if (_dogLeash.ContainsKey(name))
            {
                return _dogLeash[name] as T;
            }
            else if (_catFood.ContainsKey(name))
            {
                return _catFood[name] as T;
            }
            else
                return null;
          }   
        public decimal GetTotalPriceOfInventory(){
            return _products.InStock().Sum(x=>x.Price*x.Quantity) ?? 0;
        }
        public void DefineNewProduct(UILogic uiLogic)
        {
            Console.WriteLine("Press 1 to add Cat Food or 2 to Add a Dog Leash");
            int typeSelection = uiLogic.GetValidUserSelection(new List<int> { 1, 2 });
            Console.WriteLine("Enter a JSON string that describes the product:");
            string json = Console.ReadLine();
            ProductValidator validator = new ProductValidator();
            switch (typeSelection)
            {
                case 1:
                    CatFood catFood = JsonSerializer.Deserialize<CatFood>(json);
                    validator.ValidateAndThrow(catFood);
                    this.AddProductToDictionary(catFood);
                    break;
                case 2:
                    DogLeash dogLeash = JsonSerializer.Deserialize<DogLeash>(json);
                    validator.ValidateAndThrow(dogLeash);
                    this.AddProductToDictionary(dogLeash);
                    break;
                default:
                    break;
            }

        }
    }
}