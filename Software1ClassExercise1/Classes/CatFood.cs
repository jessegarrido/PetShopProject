﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class CatFood : Product
    {
        public double WeightPounds { get; set; } = 0;
        public bool KittenFood { get; set; } = false;
    }
}