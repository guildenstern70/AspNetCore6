/*
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data.Models;
using AspNetCore6.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore6.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonController> _logger;
    
    public PersonController(IPersonRepository personRepository,
        ILogger<PersonController> logger)
    {
        this._logger = logger;
        this._personRepository = personRepository;
    }
    
    /// <summary>
    /// Get a list of persons
    /// </summary>
    /// <returns>A List of persons</returns>
    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get()
    {
        this._logger.LogInformation("GET api/person");
        List<Person> result = await this._personRepository.GetAll();
        return this.Ok(result);
    }

    /// <summary>
    /// Get person by person ID
    /// </summary>
    /// <param name="id">The ID of person to find</param>
    /// <returns>The found person or 404 if not found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Person>> GetById(int id)
    {
        this._logger.LogInformation("GET api/person by ID=" + id);
        var person = await this._personRepository.GetById(id);
        if (person == null)
        {
            return this.NotFound();
        }

        return this.Ok(person);
    }

    /// <summary>
    /// Add new person to the database
    /// </summary>
    /// <param name="person">JSON representation of person</param>
    /// <example>
    /// {
    ///        "name": "Gino",
    ///        "surname": "Pirotti",
    ///        "age": 34,
    ///        "fiscalCode": "GINPIR70M26F205T"
    /// }
    /// </example>
    /// <returns>201 if the person was successfully created</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Person>> Post(Person person)
    {
        this._logger.LogInformation("POST api/person");
        var createdPerson = await this._personRepository.AddPerson(person);
        return this.CreatedAtAction(nameof(this.Get), new { id = createdPerson.Id }, createdPerson);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Person>> Put(int id, Person person)
    {
        this._logger.LogInformation("PUT api/person with ID=" + person.Id);
        if (id != person.Id)
            return this.BadRequest("Person ID mismatch");

        var updatedPerson = await this._personRepository.ModifyPerson(person);

        if (updatedPerson == null)
            return this.NotFound($"Person with Id = {id} not found");

        return updatedPerson;
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        this._logger.LogInformation("DELETE api/person by ID=" + id);
        await this._personRepository.DeletePerson(id);
    }

}