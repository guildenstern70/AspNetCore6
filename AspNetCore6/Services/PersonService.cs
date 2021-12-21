/*
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data;
using AspNetCore6.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore6.Services;

public class PersonService: IPersonService
{
    private readonly ILogger<PersonService> _logger;
    private readonly ProjectDbContext _dbContext;
    
    public PersonService(ILogger<PersonService> logger,
                         ProjectDbContext projectDbContext)
    {
        this._logger = logger;
        this._dbContext = projectDbContext;
    }

    public async Task<List<Person>> GetAll()
    {
        await using var context = this._dbContext;
        return await context.Persons.ToListAsync();
    }

    public async Task<Person?> GetById(int id)
    {
        await using var context = this._dbContext;
        return await context.Persons.FindAsync(id);
    }

    public async Task<Person> AddPerson(Person p)
    {
        await using var context = this._dbContext;
        context.Persons.Add(p);
        await context.SaveChangesAsync();
        return p;
    }

    public async Task<Person?> ModifyPerson(Person p)
    {
        await using var context = this._dbContext;
        var person = await context.Persons.FindAsync(p.Id);
        if (person == null)
        {
            return null;
        }
        person.Name = p.Name;
        person.Surname = p.Surname;
        person.Age = p.Age;
        person.FiscalCode = p.FiscalCode;
        await context.SaveChangesAsync();
        return person;
    }

    public async Task DeletePerson(int id)
    {
        await using var context = this._dbContext;
        var person = await context.Persons.FindAsync(id);
        if (person == null) return;
        context.Remove(person);
        await context.SaveChangesAsync();
    }

    public long Size()
    {
        return this._dbContext.Persons?.Count() ?? 0L;
    }
    
    
}
