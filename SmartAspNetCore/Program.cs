/**
 * 
 * Project SmartAspNetCore
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 **/

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Home Page
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Controllers
builder.Services.AddControllers();

// Swagger https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Logging
builder.Services.AddLogging(options =>
{
    options.AddSimpleConsole(c =>
    {
        c.TimestampFormat = "[dd-MM-yyyy HH:mm:ss.fff] ";
    });
});

WebApplication app = builder.Build();

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
