using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        { 
            RuleFor(x => x.Customer)
                .NotEmpty()
                .WithMessage("O Cliente é obrigatório.");

            RuleFor(x => x.Branch)
                .NotEmpty()
                .WithMessage("A Filial é obrigatória.");

            RuleForEach(x => x.Items)
                .SetValidator(new CreateSaleItemRequestValidator());
        }
    }

    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(x => x.Product)
                .NotEmpty()
                .WithMessage("O Produto é obrigatório.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("A Quantidade deve ser maior que 0.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("O Preço unitário deve ser maior que 0.");
        }
    }
}
