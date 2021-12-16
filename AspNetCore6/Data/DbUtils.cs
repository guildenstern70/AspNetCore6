/**
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 **/

using AspNetCore6.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AspNetCore6.Data;

public static class DbUtils
{

    /// <summary>
    /// Method to populate the database
    /// </summary>
    /// <param name="options">The configured options.</param>
    /// <param name="count">The number of contacts to seed.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public static async Task EnsureDbCreatedAndSeedAsync(
        DbContextOptions<ProjectDbContext> options, int count)
    {
        Debug.WriteLine("Seeding DB");
        var builder = new DbContextOptionsBuilder<ProjectDbContext>(options);

        await using var context = new ProjectDbContext(builder.Options);
        
        PopulateDb(context);
    }
    
    private static void PopulateDb(ProjectDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();
        
        if (dbContext.Persons.Any())
        {
            Debug.WriteLine("DB already populated");
            return;   // DB has been seeded
        }

        Debug.WriteLine("Populating database...");
        dbContext.Add(new Person { Name = "Alessio", Surname = "Saltarin", Age = 47, FiscalCode = "SLTLSS70M26F205X" });
        dbContext.Add(new Person { Name = "Elena", Surname = "Zambrelli", Age = 27, FiscalCode = "ZMBELE75F32M200F" });
        dbContext.Add(new Person { Name = "Giovanni", Surname = "Rossi", Age = 43, FiscalCode = "ROSGIO67M13K204X" });
        dbContext.Add(new Person { Name = "Mauro", Surname = "Sangiovanni", Age = 21, FiscalCode = "SGIMRO71G12Y607L" });
        dbContext.SaveChanges();
        Debug.WriteLine("DB Initialization DONE");
    }
    
}