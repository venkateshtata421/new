using System;

namespace Examples.Api.Fluent.Models
{
  public class Foo
  {
    public Guid FooId { get; set; }

    public DateTimeOffset FooCreateDateTimeOffset { get; set; }

    public int FooIntValue { get; set; }

    public string FooStringValue { get; set; }
  }
}