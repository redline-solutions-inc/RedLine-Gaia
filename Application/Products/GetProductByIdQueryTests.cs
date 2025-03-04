using System;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.Features.Products.Queries;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Errors;
using RedLine_Gaia.Domain.Interfaces;

namespace Application.Products;

public class GetProductByIdQueryTest
{
    private readonly GetProductByIdQuery Query = new GetProductByIdQuery(1);
    private readonly GetProductByIdQueryHandler _handler;
    private readonly IProductRepository _productRepositoryMock;

    public GetProductByIdQueryTest()
    {
        _productRepositoryMock = Substitute.For<IProductRepository>();
        _handler = new GetProductByIdQueryHandler(_productRepositoryMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenProductNotFound()
    {
        // Arrange
        var error = new ProductNotFoundError();
        _productRepositoryMock.GetById(Arg.Is<int>(x => x == Query.id)).ReturnsNull();

        // Act
        ResultDto<ProductDTO> result = await _handler.Handle(Query, default);

        // Assert
        result.Errors.Should().HaveCount(1);
        result.Errors.FirstOrDefault().Should().NotBeNull();
        result.Errors.FirstOrDefault().Message.Should().Be(error.Message);
    }

    [Fact]
    public async Task Handle_Should_ReturnProduct_WhenCorrectProductIdGiven()
    {
        // Arrange
        Product product = new Product { Id = Query.id };
        _productRepositoryMock.GetById(Arg.Is<int>(x => x == Query.id)).Returns(product);

        // Act

        // Assert

        throw new NotImplementedException();
    }
}
