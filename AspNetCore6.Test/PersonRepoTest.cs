/*
 * 
 * Project AspNetCore6 - Unit Tests
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data.Models;
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
    public void CheckDbExistsTest()
    {
        var dbFile = Path.Join(".", "aspnetcore6.db");
        File.Exists(dbFile).Should().BeTrue();
    }

    [Fact]
    public void CheckDbIsPopulatedTest()
    {
        var dbSize = this._personRepository.Size();
        dbSize.Should().BePositive();
    }
    
    [Fact]
    public async void FindPersonsTest()
    {
        var persons = await this._personRepository.GetAll();
        persons.Count.Should().BePositive();
    }

    [Fact]
    public async void AddNewPersonTest()
    {
        var person = new Person {Name = "Pippo", Surname = "Balzaroni", Age = 36, FiscalCode = "PIPBLZ71M12F345G"};
        await this._personRepository.AddPerson(person);
        var personFound = await this._personRepository.FindPersonsByNameAndSurname("Pippo", "Balzaroni");
        personFound.Count.Should().Be(1);
        personFound[0].FiscalCode.Should().Be("PIPBLZ71M12F345G");
        // Clean Up
        await this._personRepository.DeletePerson(personFound[0].Id);
    } 
}
