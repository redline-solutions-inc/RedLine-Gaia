using NSubstitute;
using RedLine_Gaia.Domain.Interfaces;

namespace Application.Products;

public class UpdateProductCommandTests
{
    private readonly IProductRepository _productRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public UpdateProductCommandTests()
    {
        _productRepositoryMock = Substitute.For<IProductRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    }

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
