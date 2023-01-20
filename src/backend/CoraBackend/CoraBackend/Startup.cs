using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CoraBackend
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment, ILogger<Startup> logger) {
            this.configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddEndpointsApiExplorer();

            if (environment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cora Backend", Version = "v1"});
                });
            }
        }

        // Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    Predicate = (_) => false,
                });
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                });
            });
        }
    }
}
