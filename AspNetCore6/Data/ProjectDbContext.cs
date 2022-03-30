/*
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore6.Data;

public sealed class ProjectDbContext : DbContext
{
    // Do not delete set accessor! It is used by Entity Framework
    public DbSet<Person> Persons { get; set; }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
        this.Persons = this.Set<Person>();
    }

}