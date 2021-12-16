/**
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 **/

using AspNetCore6.Data;
using AspNetCore6.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Home Page
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Services tied to HTTP Session
builder.Services.AddScoped<IPersonService, PersonService>();

// Controllers
builder.Services.AddControllers();

// Swagger https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core 
builder.Services.AddDbContextFactory<ProjectDbContext>(opt =>
{
    opt.UseSqlite($"Data Source={ProjectDbContext.DbPath}");
});

// Logging
builder.Services.AddLogging(options =>
{
    options.AddSimpleConsole(c =>
    {
        c.TimestampFormat = "[dd-MM-yyyy HH:mm:ss.fff] ";
    });
});

// Routing is lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

WebApplication app = builder.Build();

// Populate DB
await using AsyncServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
var options = scope.ServiceProvider.GetRequiredService<DbContextOptions<ProjectDbContext>>();
await DbUtils.EnsureDbCreatedAndSeedAsync(options, 10);

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); //Routes for my API controllers
    endpoints.MapBlazorHub();   //Routes for pages
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
