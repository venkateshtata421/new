using System;
using Examples.Api.Fluent.Requests;
using Examples.Api.Fluent.Validators.Errors;
using FluentValidation;

namespace Examples.Api.Fluent.Validators
{
  public class FooCreateRequestValidator : AbstractValidator<FooCreateRequest>
  {
    public FooCreateRequestValidator()
    {
      this.RuleFor(x => x.FooStringValue)
        .NotEmpty()
        .WithMessage(FooErrors.FooStringValueRequired)
        .NotNull()
        .WithMessage(FooErrors.FooStringValueRequired)
        .DependentRules(() =>
        {
          this.RuleFor(x => x.FooStringValue)
            .Must(x => x.StartsWith("bar", StringComparison.InvariantCultureIgnoreCase))
            .WithMessage(FooErrors.FooStringValueStartBar);
        });
        

      this.RuleFor(x => x.FooIntValue)
        .GreaterThanOrEqualTo(42)
        .WithMessage(FooErrors.FooIntValueMustBeGreaterThanOrEqual42);
    }
  }
}