/*
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data;
using AspNetCore6.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Home Page
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Controllers
builder.Services.AddControllers();

// Swagger https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AspNetCore6 API",
        Description = "An ASP.NET Core Web API Template",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });
    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// EF Core 
builder.Services.AddDbContextFactory<ProjectDbContext>(opt =>
{
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    {
        Debug.WriteLine("Using SQLite Development DB");
        opt.UseSqlite(builder.Configuration.GetConnectionString("Lite"));
    }
    else
    {
        Debug.WriteLine("Using PostgreSQL Production DB");
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Elephant"));
    } 
});

// Logging
builder.Services.AddLogging(options =>
{
    options.AddSimpleConsole(c =>
    {
        c.TimestampFormat = "[dd-MM-yyyy HH:mm:ss.fff] ";
    });
});

// Routing must be lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

WebApplication app = builder.Build();

// Populate DB
await using AsyncServiceScope scope = 
    app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<ProjectDbContext>>();
await DbUtils.EnsureDbCreatedAndSeedAsync(options, 10);

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.InjectStylesheet("/css/swaggerui/theme-flattop.css");
});
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); //Routes for my API controllers
    endpoints.MapBlazorHub();   //Routes for pages
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
