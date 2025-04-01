using FluentValidation.TestHelper;
using RedLine_Gaia.Application.Features.Products.Commands;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.Features.Products.Validators;

namespace RedLine_Gaia.Tests.Application_Layer.Products;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _createProductValidator;
    private readonly CreateProductCommand Command = new CreateProductCommand(
        new ProductDTO
        {
            Name = "Product Name",
            Description = "Some Text",
            Price = 1,
        }
    );

    public CreateProductCommandValidatorTests()
    {
        _createProductValidator = new CreateProductCommandValidator();
    }

    [Fact]
    public void Validator_Should_Pass_WhenProductHasAllCorrectValidation()
    {
        // Act
        var response = _createProductValidator.TestValidate(Command);

        // Assert
        response.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_Should_Fail_WhenProductNameIsEmpty()
    {
        // Arrange
        ProductDTO product = new ProductDTO
        {
            Name = "",
            Description = "",
            Price = 1,
        };
        CreateProductCommand invalidCommand = Command with { dto = product };

        // Act
        var response = _createProductValidator.TestValidate(invalidCommand);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.dto.Name).Only();
    }

    [Fact]
    public void Validator_Should_Fail_WhenProductNameIsExceedsMax30Chars()
    {
        // Arrange
        ProductDTO product = new ProductDTO
        {
            Name = "This is a really long name that should not work",
            Description = "",
            Price = 1,
        };
        CreateProductCommand invalidCommand = Command with { dto = product };

        // Act
        var response = _createProductValidator.TestValidate(invalidCommand);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.dto.Name).Only();
    }

    [Fact]
    public void Validator_Should_Fail_WhenProductPriceIsEmpty()
    {
        // Arrange
        ProductDTO product = new ProductDTO { Name = "Test" };
        CreateProductCommand invalidCommand = Command with { dto = product };

        // Act
        var response = _createProductValidator.TestValidate(invalidCommand);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.dto.Price).Only();
    }

    [Fact]
    public void Validator_Should_Fail_WhenProductPriceIsZeroOrLess()
    {
        // Arrange
        ProductDTO product = new ProductDTO { Name = "Test", Price = -1 };
        CreateProductCommand invalidCommand = Command with { dto = product };

        // Act
        var response = _createProductValidator.TestValidate(invalidCommand);

        // Assert
        response.ShouldHaveValidationErrorFor(x => x.dto.Price).Only();
    }
}
