using System;
using System.Collections.Generic;
using Examples.Api.Fluent.Models;

namespace Examples.Api.Fluent.Repository
{
  public static class FooRepository
  {
    public static readonly Dictionary<Guid, Foo> AllFoos = new Dictionary<Guid, Foo>();
  }
}