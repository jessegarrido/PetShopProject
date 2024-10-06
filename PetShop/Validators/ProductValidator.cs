using FluentValidation;
using PetStore;

namespace PetShop
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => decimal.Parse(x.Price.ToString().Trim('$'))).GreaterThanOrEqualTo(0);
          //  RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).Must(x => x == null || x.Length >= 10);
            }
        }
    }
