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

    public Person AddPerson(Person p)
    {
        this._dbContext.Add(p);
        this._dbContext.SaveChanges();
        return p;
    }

    public void DeletePerson(int id)
    {
        Person? person = this._dbContext.Persons?.Find(id);
        if (person == null) return;
        
        this._dbContext.Remove(person);
        this._dbContext.SaveChanges();
    }

    public long Size()
    {
        return this._dbContext.Persons?.Count() ?? 0L;
    }

    public Person? GetPerson(int id)
    {
        return this._dbContext.Persons?.Find(id);
    }
    
}
