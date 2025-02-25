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

public class UpdateProductCommandTests
{
    [Fact]
    public Task Handle_Should_ReturnError_WhenProductNotFound()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public Task Handle_Should_ReturnSuccess_WhenValidProductIsGiven()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public Task Handle_Should_ReturnError_WhenProductNameAlreadyTaken()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public Task Handle_Should_CallRepository_WhenProductNameIsValid()
    {
        throw new NotImplementedException();
    }
}
