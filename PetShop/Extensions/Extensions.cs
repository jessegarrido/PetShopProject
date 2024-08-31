using PetShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;
using FluentValidation;

namespace Petshop
{
    static class ListExtensions
    {
        public static IList<T> InStock<T>(this IList<T> list) where T : Product
        {
            return list.Where(x => x.Quantity > 0).ToList();
        }
    }
}
