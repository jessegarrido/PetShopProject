using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class DogLeash : Product
    {
        public int LengthInches { get; set; } = 0;
        public string Material { get; set; } = string.Empty;
    }
}
