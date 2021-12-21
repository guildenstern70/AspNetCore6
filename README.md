# ASP.NET Core 6

ASP.NET Core 6 REST API Template with

* Home Page in Bootstrap 5
* ORM with Entity Framework and SQLite
* Full Swagger documentation

### What is needed

This project runs with .NET 6 (formerly known as .NET Core)

.NET Download Page:
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

### XML Documentation

Swagger UI reads documentation from XML file generated from XML comments. CS Project must have this modification:

    <PropertyGroup>
    [...]
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
    [...]
    </PropertyGroup>

### Application Version

Application Version is set in the Properties in CS Project

    <PropertyGroup>
    [...]
    <Version>1.2.3.4</Version>
    [...]
    </PropertyGroup>


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




