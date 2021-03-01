using System.ComponentModel;

namespace Examples.Api.Fluent.Validators.Errors
{
  public class FooErrors
  {
    [Description("Foo string value must start with 'bar'.")]
    public const string FooStringValueStartBar = "FOO_CREATE_MUST_START_BAR";

    [Description("Foo string value is requred.")]
    public const string FooStringValueRequired = "FOO_STRING_VALUE_REQUIRED";

    [Description("Foo int value must be greater than or equal 42.")]
    public const string FooIntValueMustBeGreaterThanOrEqual42 = "FOO_CREATE_MUST_BE_GREATER_THAN_OR_EQUAL_42";
  }
}