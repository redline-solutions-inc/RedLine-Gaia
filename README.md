# RedLine-Gaia API Project

This repository contains an example API project used as the base for RedLine Web APIs. This project was built using the Clean Architecture principles, Repository Pattern, Unit of Work Pattern, Scalar, MediatR, Mapter, FluentResults, and Entity Framework. The project also includes unit tests using xUnit, FluentAssertions, and Nsubstitute.

## Project Structure

The project follows the Onion Architecture, which means that the codebase is organized into layers, with the domain model at the center and the outer layers dependent on the inner layers.

The project has the following structure:

- **Domain**: Contains the domain model, which represents the core business logic of the application. It includes entities, and value objects.
- **Application**: Contains the application layer, which implements the use cases of the system. Each feature is organized to include its commands, queries, handlers, and DTOs.
- **Infrastructure**: Contains the infrastructure layer, which implements the technical details of the system. It includes database access, logging, configuration, and external services.
- **API**: Contains the presentation layer, which exposes the functionality of the system to the outside world. It primarily consists of controllers.

## Multi-Tenant (Seperate Databases w/ Shared Schema)

This project is a multi-tenant architecture. Each tenant has a separate database and they all share the same schema. A **Master** database manages all tenant information, including connection strings, and acts as a `Tenant Registry`.

- **Middleware / Action Filter**: Middleware is used to retrieve tenant information from the request before a controller Commands or Queries are called.
- **CurrentTenantService**: A scoped service designed to house a requests tenant information and can be injected into required repositories, services, or units-of-work.
- **TenantService**: A singleton service that is responsible for retrieving tenant information from the Tenant Registry.
- **DbContextFactory**: An injectable class responsible for creating a tenant specific DbContext for calling repositories, services, or units-of-work.

## Migrations

With a multi-tenant architecture, MasterDb & TenantDb migrations have seperate file locations. MasterDb entity changes should follow the following add migrations command to make sure the output is in the Migrations/TenantMaster folder:

`add-migration NameOfMigration -context MasterDbContext -o Migrations/TenantMaster`

TenantDb migration can be executed as per usual and will output into the root Migrations folder.

## Dependencies

The project uses the following dependencies:

- **MediatR**: A lightweight library that provides a mediator pattern implementation for .NET.
- **Entity Framework Core**: A modern object-relational mapper for .NET that provides data access to the application.
- **FluentValidation**: A validation library that provides a fluent API for validating objects.
- **Mapster**: A library that maps one object type to a different type making entity to DTO mapping fast and consistant.
- **Scalar**: Provides an easy way to render beautiful API references based on OpenAPI/Swagger documents
- **Serilog**: A diagnostic logging library
- **BetterStack**: Remote log storage and monitor.

## Running the Project

To run the project, follow these steps:

1. Clone the repository to your local machine.
2. Open the solution in your IDE of choice.
3. Build the solution to restore the dependencies.
4. Update the connection string `MasterDbConnection` in the `appsettings.json` file to point to your Tenant Master database.
5. Run `TenantDatabaseMigrater` console app to run migrations.
   1. Create Additional Tenant Databases and add their connection strings to the `Tenant` table of your `MasterDbConnection` db
   2. Rerun `TenantDatabaseMigrater` if any Tenant Databases were created.
6. (Optional) To use BetterStack set the `sourceToken` and `betterStackEndpoint` in the `appsettings.json` to the configured BetterStack source.
7. Start the API project
8. The API should be accessible at `https://localhost:<port>/scalar/v1` where `<port>` is the port number specified in the project properties

## Running the Tests

To run the tests, follow these steps:

1. Open the solution in your IDE of choice.
2. Build the solution to restore the dependencies.
3. Open the Test Explorer window
4. Run all the tests by clicking the Run All button in the Test Explorer.
