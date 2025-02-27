using FluentAssertions;
using FluentResults;
using NSubstitute;
using RedLine_Gaia.Application.Features.Products.Commands;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Errors;
using RedLine_Gaia.Domain.Interfaces;

namespace Application.Products;

public class CreateProductCommandTests
{
    private readonly CreateProductCommand Command = new CreateProductCommand(
        new ProductDTO { Name = "Test" }
    );
    private readonly CreateProductCommandHandler _handler;
    private readonly IProductRepository _productRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateProductCommandTests()
    {
        _productRepositoryMock = Substitute.For<IProductRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateProductCommandHandler(_productRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenProductNameIsNull()
    {
        // Arrange
        CreateProductCommand invalidCommand = Command with
        {
            dto = new ProductDTO { Name = null },
        };
        var error = new ProductNameEmptyError();

        // Act
        ResultDto<int> result = await _handler.Handle(invalidCommand, default);

        // Assert
        result.Errors.Should().HaveCount(1);
        result.Errors.FirstOrDefault().Should().NotBeNull();
        result.Errors.FirstOrDefault().Message.Should().Be(error.Message);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenProductNameIsEmpty()
    {
        // Arrange
        CreateProductCommand invalidCommand = Command with
        {
            dto = new ProductDTO { Name = " " },
        };
        var error = new ProductNameEmptyError();

        // Act
        ResultDto<int> result = await _handler.Handle(invalidCommand, default);

        // Assert
        result.Errors.Should().HaveCount(1);
        result.Errors.FirstOrDefault().Should().NotBeNull();
        result.Errors.FirstOrDefault().Message.Should().Be(error.Message);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenProductNameIsNotUnique()
    {
        // Arrange
        var error = new ProductNameMustBeUniqueError();
        _productRepositoryMock
            .IsProductNameUnique(Arg.Is<Product>(e => e.Name == Command.dto.Name))
            .Returns(false);

        // Act
        ResultDto<int> result = await _handler.Handle(Command, default);

        // Assert
        result.Errors.Should().HaveCount(1);
        result.Errors.FirstOrDefault().Should().NotBeNull();
        result.Errors.FirstOrDefault().Message.Should().Be(error.Message);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenProductNameIsUnique()
    {
        // Arrange
        _productRepositoryMock
            .IsProductNameUnique(Arg.Is<Product>(e => e.Name == Command.dto.Name))
            .Returns(true);
        _productRepositoryMock.Add(Arg.Is<Product>(e => e.Name == Command.dto.Name));

        // Act
        ResultDto<int> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallRepositoryCreate_WhenProductNameIsUnique()
    {
        // Arrange
        _productRepositoryMock
            .IsProductNameUnique(Arg.Is<Product>(e => e.Name == Command.dto.Name))
            .Returns(true);
        _productRepositoryMock.Add(Arg.Is<Product>(e => e.Name == Command.dto.Name));

        // Act
        ResultDto<int> result = await _handler.Handle(Command, default);

        // Assert
        _productRepositoryMock.Received(1).Add(Arg.Any<Product>());
    }
}
