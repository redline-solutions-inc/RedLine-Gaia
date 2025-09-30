using FluentValidation;
using RedLine_Gaia.Application.Features.Products.Commands;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Domain.Models;

namespace RedLine_Gaia.Application.Features.Products.Validators;

/// <summary>
/// Validator for the CreateProductCommand.
/// Uses the current tenant service to get the locale and return the appropriate error message.
/// </summary>
/// <param name="currentTenantService">Current tenant service</param>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(ICurrentTenantService currentTenantService)
    {
        RuleFor(p => p.dto.Name)
            .NotEmpty()
            .WithMessage(x =>
                currentTenantService.Locale switch
                {
                    Locale.EN => "Product name can not be empty",
                    Locale.ES => "El nombre del producto no puede estar vacío",
                    Locale.FR => "Le nom du produit ne peut pas être vide",
                    _ => "Product name can not be empty",
                }
            );

        RuleFor(p => p.dto.Name)
            .MaximumLength(30)
            .WithMessage(x =>
                currentTenantService.Locale switch
                {
                    Locale.EN => "Product name legth can not exceed 30 characters",
                    Locale.ES =>
                        "La longitud del nombre del producto no puede exceder los 30 caracteres",
                    Locale.FR => "La longueur du nom du produit ne peut pas dépasser 30 caractères",
                    _ => "Product name legth can not exceed 30 characters",
                }
            );

        RuleFor(p => p.dto.Price)
            .NotEmpty()
            .WithMessage(x =>
                currentTenantService.Locale switch
                {
                    Locale.EN => "Price must be greater then zero",
                    Locale.ES => "El precio debe ser mayor que cero",
                    Locale.FR => "Le prix doit être supérieur à zéro",
                    _ => "Price must be greater then zero",
                }
            );

        RuleFor(p => p.dto.Price)
            .GreaterThan(0)
            .WithMessage(x =>
                currentTenantService.Locale switch
                {
                    Locale.EN => "Price must be greater then zero",
                    Locale.ES => "El precio debe ser mayor que cero",
                    Locale.FR => "Le prix doit être supérieur à zéro",
                    _ => "Price must be greater then zero",
                }
            );
    }
}
