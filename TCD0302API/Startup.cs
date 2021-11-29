using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TCD0302API.Data;
using TCD0302API.Mapper;
using TCD0302API.Repositories;
using TCD0302API.Repositories.Interfaces;

namespace TCD0302API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // Add Repositories services
      services.AddScoped<IParkRepository, ParkRepository>();

      // Add Mapping
      services.AddAutoMapper(typeof(ApiMapping));

      // Configure Swagger services
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1",
          new Microsoft.OpenApi.Models.OpenApiInfo()
          {
            Title = "TCD0302 Api",
            Version = "v1",
          });
      });

      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      // Use Swagger
      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TCD0302 Api");
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
