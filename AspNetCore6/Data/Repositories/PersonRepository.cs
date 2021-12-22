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

namespace AspNetCore6.Data.Repositories;

public class PersonRepository: IPersonRepository
{
    private readonly ILogger<PersonRepository> _logger;
    private readonly ProjectDbContext _dbContext;

    public PersonRepository(ILogger<PersonRepository> logger,
                         ProjectDbContext projectDbContext)
    {
        this._logger = logger;
        this._dbContext = projectDbContext;
    }

    public ProjectDbContext GetDbContext()
    {
        return this._dbContext;
    }
    
    ~PersonRepository()  // finalizer
    {
        // Cleaning Up Context
        this._dbContext.Dispose();
    }

    public async Task<List<Person>> GetAll()
    {
        return await this._dbContext.Persons.ToListAsync();
    }

    public async Task<Person?> GetById(int id)
    {
        return await this._dbContext.Persons.FindAsync(id);
    }

    public async Task<Person> AddPerson(Person p)
    {
        this._dbContext.Persons.Add(p);
        await this._dbContext.SaveChangesAsync();
        return p;
    }

    public async Task<List<Person>> FindPersonsByNameAndSurname(string name, string surname)
    {
        return await this._dbContext.Persons
                    .Where(s => s.Name == name && s.Surname == surname)
                    .ToListAsync();
    }

    public async Task<Person?> ModifyPerson(Person p)
    {
        var person = await this._dbContext.Persons.FindAsync(p.Id);
        if (person == null)
        {
            return null;
        }
        person.Name = p.Name;
        person.Surname = p.Surname;
        person.Age = p.Age;
        person.FiscalCode = p.FiscalCode;
        await this._dbContext.SaveChangesAsync();
        return person;
    }

    public async Task DeletePerson(int id)
    {
        var person = await this._dbContext.Persons.FindAsync(id);
        if (person == null) return;
        this._dbContext.Remove(person);
        await this._dbContext.SaveChangesAsync();
    }

    public long Size()
    {
        return this._dbContext.Persons?.Count() ?? 0L;
    }
    
    
}
