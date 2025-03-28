# RedLine-Gaia Testing

This testing project is designed to **Unit Test the business logic** defined in the applications **Commands & Queries** (CQRS Pattern) and any **Validators**. Tests are broken into feature folders with test classes named after their corresponding Command/Query/Validator. Each test is written to be lean.

## Mocking Repository & Interfaces

[NSubstitute](https://nsubstitute.github.io/) is used to mock the repositories and other interfaces.The following example deminstrates a new test class and constructing mocks of necessary components:

```
public class UpdateProductCommandTests
{
    private readonly IProductRepository _productRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public UpdateProductCommandTests()
    {
        _productRepositoryMock = Substitute.For<IProductRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    }
}
```

## Test Structure

Tests follow the naming scheme:

`<NameOfMethod>_Should_<WhatShouldHappan>_<Condition>`

Tests are written following the Arrange-Act-Assert pattern:

- **Arrange** inputs and targets.
- **Act** on the target behavior.
- **Assert** expected outcomes.
