/**
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 **/

using AspNetCore6.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore6.Data;

public class ProjectDbContext : DbContext
{
    // Do not delete set accessor! It is used by Entity Framework
    public DbSet<Person> Persons { get; set; }

    public static readonly string DbPath = Path.Join(".", "aspnetcore6.db");

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    // The following configures EF to create a Sqlite database file in the
    // current path
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

}