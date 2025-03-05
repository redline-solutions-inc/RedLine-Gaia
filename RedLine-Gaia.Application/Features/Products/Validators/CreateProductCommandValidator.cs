using FluentValidation;
using RedLine_Gaia.Application.Features.Products.Commands;

namespace RedLine_Gaia.Application.Features.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.dto.Name).NotEmpty().WithMessage("Product name can not be empty");

        RuleFor(p => p.dto.Name)
            .MaximumLength(30)
            .WithMessage("Product name legth can not exceed 30 characters");

        RuleFor(p => p.dto.Price).NotEmpty().WithMessage("Price must be greater then zero");

        RuleFor(p => p.dto.Price).GreaterThan(0).WithMessage("Price must be greater then zero");
    }
}
