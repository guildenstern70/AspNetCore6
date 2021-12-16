# ASP.NET Core 6

ASP.NET Core 6 REST API Template with

* Hero start page in Bootstrap 5
* ORM with Entity Framework and SQLite
* Full Swagger documentation

### Entity Framework Core setup

Install Entity Framework

    dotnet tool install --global dotnet-ef
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.Tools

If you need to update the Entity Framework core:

    dotnet tool update --global dotnet-ef

### DB Migrations

    cd AspNetCore6
    dotnet ef migrations add InitialCreate
    dotnet ef database update

To remove existing migrations:

    dotnet ef database remove




