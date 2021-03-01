using Examples.Api.Fluent.Requests;
using Examples.Api.Fluent.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Examples.Api.Fluent.Tests.ValidatorTests
{
  public class FooCreateRequestValidatorTests
  {
    private readonly FooCreateRequestValidator _validator;

    public FooCreateRequestValidatorTests()
    {
      _validator = new FooCreateRequestValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenStringIsNull()
    {
      var request = new FooCreateRequest();
      var result = _validator.TestValidate(request);

      result.ShouldHaveValidationErrorFor(x => x.FooStringValue);
    }

    [Fact]
    public void ShouldHaveErrorWhenStringDoesNotStartBar()
    {
      var request = new FooCreateRequest
      {
        FooStringValue = "bazIShouldFail"
      };

      var result = _validator.TestValidate(request);
      result.ShouldHaveValidationErrorFor(x => x.FooStringValue);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenStringStartsWithBar()
    {
      var request = new FooCreateRequest
      {
        FooStringValue = "barIShouldPass"
      };

      var result = _validator.TestValidate(request);
      result.ShouldNotHaveValidationErrorFor(x => x.FooStringValue);
    }

    [Fact]
    public void ShouldHaveErrorWhenIntLessThan42()
    {
      var request = new FooCreateRequest
      {
        FooIntValue = 41
      };

      var result = _validator.TestValidate(request);
      result.ShouldHaveValidationErrorFor(x => x.FooIntValue);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenIntEquals42()
    {
      var request = new FooCreateRequest
      {
        FooIntValue = 42
      };

      var result = _validator.TestValidate(request);
      result.ShouldNotHaveValidationErrorFor(x => x.FooIntValue);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenIntGreaterThan42()
    {
      var request = new FooCreateRequest
      {
        FooIntValue = 100
      };

      var result = _validator.TestValidate(request);
      result.ShouldNotHaveValidationErrorFor(x => x.FooIntValue);
    }

    [Fact]
    public void ShouldNotHaveAnyErrorsWithValidRequest()
    {
      var request = new FooCreateRequest
      {
        FooIntValue = 42,
        FooStringValue = "Bar is the answer"
      };

      var result = _validator.TestValidate(request);
      result.ShouldNotHaveAnyValidationErrors();
    }
  }
}