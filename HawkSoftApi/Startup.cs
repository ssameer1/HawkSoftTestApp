using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using HawkSoftApi.Data;

namespace HawkSoftApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //So that local host request can be made from different projects application reside in different project 
        //For current situation we might not need this but just to be in safe side. 
        //For prod we might want to use Gateway url or so on.
        readonly string AllowSpecificOrigin = "orign";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HaskSoft Api", Version = "v1" });
            });

            services.AddCors(option =>
            {
                option.AddPolicy(AllowSpecificOrigin,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:51898",
                                            "https://localhost:44345/");
                    });
            });

            services.AddDbContext<UserContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("UserContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HaskSoft Api V1");
            });

            app.UseCors(AllowSpecificOrigin);
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
