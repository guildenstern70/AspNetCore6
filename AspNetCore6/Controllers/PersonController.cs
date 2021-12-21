/*
 * 
 * Project AspNetCore6
 * Copyright (C) 2021 Alessio Saltarin 'alessiosaltarin@gmail.com'
 * This software is licensed under MIT License. See LICENSE.
 * 
 */

using AspNetCore6.Data.Models;
using AspNetCore6.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;
    
    public PersonController(IPersonService personService,
        ILogger<PersonController> logger)
    {
        this._logger = logger;
        this._personService = personService;
    }
    
    /// <summary>
    /// Get a list of persons
    /// </summary>
    /// <returns>A List of persons</returns>
    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get()
    {
        this._logger.LogInformation("GET api/person");
        List<Person> result = await this._personService.GetAll();
        return this.Ok(result);
    }

}