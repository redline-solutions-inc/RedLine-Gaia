# RedLine-Gaia API Project

This repository contains an example API project used as the base for RedLine Web APIs. This project was built using the Clean Architecture principles, Repository Pattern, Unit of Work Pattern, Scalar, MediatR, Mapter, FluentResults, and Entity Framework. The project also includes unit tests using xUnit, FluentAssertions, and Nsubstitute.

## Project Structure

The project follows the Onion Architecture, which means that the codebase is organized into layers, with the domain model at the center and the outer layers dependent on the inner layers.

The project has the following structure:

- **Domain**: Contains the domain model, which represents the core business logic of the application. It includes entities, and value objects.
- **Application**: Contains the application layer, which implements the use cases of the system. Each feature is organized to include its commands, queries, handlers, and DTOs.
- **Infrastructure**: Contains the infrastructure layer, which implements the technical details of the system. It includes database access, logging, configuration, and external services.
- **API**: Contains the presentation layer, which exposes the functionality of the system to the outside world. It primarily consists of controllers.

## Dependencies

The project uses the following dependencies:

- **MediatR**: A lightweight library that provides a mediator pattern implementation for .NET.
- **Entity Framework Core**: A modern object-relational mapper for .NET that provides data access to the application.
- **FluentValidation**: A validation library that provides a fluent API for validating objects.
- **Mapster**: A library that maps one object type to a different type making entity to DTO mapping fast and consistant.
- **Scalar**: Provides an easy way to render beautiful API references based on OpenAPI/Swagger documents

## Running the Project

To run the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution in your IDE of choice.
3. Build the solution to restore the dependencies.
4. Update the connection string `DefaultConnection` in the `appsettings.json` file to point to your database.
5. Start the API project
6. Run any DB Migrations
7. The API should be accessible at `https://localhost:<port>/scalar/v1` where `<port>` is the port number specified in the project properties

## Running the Tests

To run the tests, follow these steps:

1. Open the solution in your IDE of choice.
2. Build the solution to restore the dependencies.
3. Open the Test Explorer window
4. Run all the tests by clicking the Run All button in the Test Explorer.
