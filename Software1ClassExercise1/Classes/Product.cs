using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetShop
{
    [JsonDerivedType(typeof(Product), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(DogLeash), typeDiscriminator: "dogleash")]
    [JsonDerivedType(typeof(CatFood), typeDiscriminator: "catfood")]
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; } = 0;
        public int? Quantity { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }
    public class CatFood : Product
    {
        public double WeightPounds { get; set; } = 0;
        public bool KittenFood { get; set; } = false;
    }
    public class DogLeash : Product
    {
        public int? LengthInches { get; set; } = 0;
        public string Material { get; set; } = string.Empty;
     }
    }
