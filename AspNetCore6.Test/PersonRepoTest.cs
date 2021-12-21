/*
 * 
 * Project AspNetCore6 - Unit Tests
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace AspNetCore6.Test;

public class PersonRepoTest
{
    private readonly IPersonRepository _personRepository;
    
    public PersonRepoTest(IPersonRepository personRepo) =>
        this._personRepository = personRepo;

    [Fact]
    public void CheckDbExists()
    {
        var dbFile = Path.Join(".", "aspnetcore6.db");
        File.Exists(dbFile).Should().BeTrue();
    }

    [Fact]
    public void CheckDbIsPopulated()
    {
        var dbSize = this._personRepository.Size();
        dbSize.Should().BePositive();
    }
    
    [Fact]
    public async void FindPersons()
    {
        var persons = await this._personRepository.GetAll();
        persons.Count.Should().BePositive();
    }
}
