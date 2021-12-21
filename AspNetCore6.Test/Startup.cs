/*
 * 
 * Project AspNetCore6 - Unit Tests
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data;
using AspNetCore6.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace AspNetCore6.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureWebHost(webHostBuilder => webHostBuilder
            .UseTestServer()
            .Configure(this.Configure)
            .ConfigureServices(this.ConfigureServices));

    private void Configure(IApplicationBuilder app) =>
        app.UseRouting().UseEndpoints(endpoints => endpoints.MapControllers());
    
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddDbContextFactory<ProjectDbContext>(opt =>
        {
            var path = Path.Join(".", "aspnetcore6.db");
            opt.UseSqlite($"Data Source={path}");
        });
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}
