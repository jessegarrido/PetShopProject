using FluentValidation;
using PetStore;

namespace Petshop
{
    static class ListExtensions
    {
        public static IList<T> InStock<T>(this IList<T> list) where T : Product
        {
            return list.Where(x => (int.Parse(x.Quantity)) > 0).ToList();
        }
    }
}
