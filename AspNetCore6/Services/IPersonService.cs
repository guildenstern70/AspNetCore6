/*
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data.Models;

namespace AspNetCore6.Services;

public interface IPersonService
{
    Task<List<Person>> GetAll();
    Task<Person?> GetById(int id);
    Task<Person> AddPerson(Person p);
    Task<Person?> ModifyPerson(Person p);
    long Size();
    Task DeletePerson(int id);
}
