using System.Linq;
using Denali.Helpers.Objects.Responses;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Examples.Api.Fluent.Filters
{
  public class FluentValidatorActionFilter : IActionFilter
  {
    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (context.ModelState.IsValid)
      {
        return;
      }

      // Convert FluentValidation messages to Denali style responses
      var response = new ValidationResponse();
      var errors = context.ModelState.SelectMany(x => x.Value.Errors)
        .Select(x => new Error(x.ErrorMessage)).ToList();

      foreach (var error in errors)
      {
        // null on the metadata for now...in the future pass in more info?
        response.AddError(error.Code, null);
      }

      context.Result = response.ToResult();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      // nothing to see here.
    }
  }
}