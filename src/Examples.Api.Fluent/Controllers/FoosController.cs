using System;
using System.Collections.Generic;
using System.Linq;
using Denali.Helpers.Objects.Responses;
using Examples.Api.Fluent.Models;
using Examples.Api.Fluent.Repository;
using Examples.Api.Fluent.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examples.Api.Fluent.Controllers
{
  [Route("api/[controller]")]
  public class FoosController : ControllerBase
  {

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Foo>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Get()
    {
      var foos = FooRepository.AllFoos.Values.ToList();
      return foos.Any() ? (IActionResult) this.Ok(foos) : this.NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Foo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id) =>
      FooRepository.AllFoos.TryGetValue(id, out var foo)
        ? this.Ok(foo)
        : ResponseFactory.ResourceNotFound(id.ToString()).ToResult();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] FooCreateRequest request)
    {
      var foo = new Foo
      {
        FooId = Guid.NewGuid(),
        FooCreateDateTimeOffset = DateTimeOffset.UtcNow,
        FooIntValue = request.FooIntValue,
        FooStringValue = request.FooStringValue
      };
      
      FooRepository.AllFoos.Add(foo.FooId, foo);

      return this.CreatedAtAction(nameof(this.GetById), new {id = foo.FooId}, foo);
    }
  }
}