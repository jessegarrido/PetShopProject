using FluentValidation;
using PetStore;

namespace PetShop
{
    public class orderValidator : AbstractValidator<Order>    {
        public orderValidator()
        {
            RuleFor(x => x.OrderId).NotNull();
           // RuleFor(x => decimal.Parse(x.Price.Trim('$'))).GreaterThanOrEqualTo(0);
            //RuleFor(x => int.Parse(x.Quantity)).GreaterThanOrEqualTo(0);
           // RuleFor(x => x.Description).Must(x => x == null || x.Length >= 10);
        }
    }
}
