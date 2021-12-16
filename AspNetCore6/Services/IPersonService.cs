/**
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 **/

using AspNetCore6.Data.Models;

namespace AspNetCore6.Services;

public interface IPersonService
{
    Task<List<Person>> GetAll();
    Person AddPerson(Person p);
    long Size();
    Person? GetPerson(int id);
    void DeletePerson(int id);
}
