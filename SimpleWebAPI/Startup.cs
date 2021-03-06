using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleWebAPI.Data;
using SimpleWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace SimpleWebAPI
{
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllers();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddDbContext<PersonContext>(x => x.UseSqlServer(Configuration.GetConnectionString("PersonContext")));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleWebAPI", Version = "v1" });
                
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "SimpleWebAPI.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleWebAPI v1");                    
                    c.RoutePrefix = string.Empty;
                });

                var min = TimeSpan.FromMilliseconds(Configuration.GetValue<int>("LatencyMinimum"));
                var max = TimeSpan.FromMilliseconds(Configuration.GetValue<int>("LatencyMaximum"));
                app = UseSimulatedLatency(app, min, max);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
        public IApplicationBuilder UseSimulatedLatency(IApplicationBuilder app, TimeSpan min, TimeSpan max) {
            return app.UseMiddleware(typeof(LatencyMiddleware), min, max);
        }
    }
}
