# Tenant Database Migrater

A simple console app designed to run migrations for the Tenant Master database and all child Tenant databases. The console app utilizes the `MasterDbContext` and `ApplicationDbContext` to find and run any migrations for their specific entities.

## Setup

The `appsettings.json`, located in this project, needs the `MasterDBConnection` config value set to the database string of the **Tenant Master** database.

## Multi-Tenant (Separate Database w/ same Scheme)

`Tenant` records should already be inserted into the **Tenant Master** database. The connection string should point to the MS SQL server where the databases should/would like to live.
