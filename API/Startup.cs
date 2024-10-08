using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method is used to register services with the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            // Register controllers
            services.AddControllers();

            // Register Swagger for API documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
            });
        }

        // This method is used to define the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Development environment-specific configuration
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Show detailed error pages during development

                // Enable middleware to serve generated Swagger as a JSON endpoint
                app.UseSwagger();

                // Enable middleware to serve Swagger UI (HTML interface)
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                });
            }

            // Common middleware for all environments
            app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

            app.UseRouting(); // Enable routing

            // Enable middleware to handle authorization
            app.UseAuthorization();

            // Define endpoints (e.g., map controller actions to routes)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map attribute-routed controller endpoints
            });
        }
    }
}
