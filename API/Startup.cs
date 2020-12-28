using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Repositorys;
using DomainLayer.IRepositorys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
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
            services.AddControllers();
            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "apidocs";
                c.Title = "geo api";
                c.Version = "v1";
                c.Description = "geo api";
            });
            services.AddDbContext<GeoContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-7B94T84\\sqlexpress;Initial Catalog=geo;Integrated Security=True", b => b.MigrationsAssembly("API")));

            services.AddControllers(setup => { setup.ReturnHttpNotAcceptable = true; }).AddXmlDataContractSerializerFormatters();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IContinentRepository, ContinentRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IRiverRepository, RiverRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
