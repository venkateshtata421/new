using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Denali.Helpers.Objects.Responses;
using Denali.Startup;
using Examples.Api.Fluent.Filters;
using Examples.Api.Fluent.Validators.Errors;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Examples.Api.Fluent
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddEnvironmentVariables();
      this.Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLogging(log =>
      {
        log.AddConfiguration(this.Configuration.GetSection("Logging"));
        log.AddConsole();
        log.AddDebug();
      });

      services.AddDenali(new DenaliOptions
      {
        IncludeNScale = false
      });
      
      ErrorCodes.Register(typeof(FooErrors));

      services
        .AddMvc(opt => opt.Filters.Add(typeof(FluentValidatorActionFilter)))
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddFluentValidation(fv =>
        {
          fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });

      services.AddSwaggerGen(conf =>
      {
        conf.SwaggerDoc("v1", new OpenApiInfo {Title = "Example Fluent API", Version = "v1"});
        conf.AddFluentValidationRules();
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseNScaleImpersonationHeaders();
        app.UseSwagger();
        app.UseSwaggerUI(conf => conf.SwaggerEndpoint("/swagger/v1/swagger.json", "Example Fluent v1"));
      }
      app.UseDenali();
      app.UseMvc();
    }
  }
}
