using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Database;
using WebApplication2.Interfaces;
using WebApplication2.Resources;
using WebApplication2.Hubs;

namespace WebApplication2
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
            services.AddDbContext<Context>(opt =>
                                               opt.UseSqlServer("Server=DESKTOP-MG0K7CA;Database=ProjectDatabase;Trusted_Connection=True;MultipleActiveResultSets=true")); ;

            services.AddScoped<IGenericInterface,UserResources>();
            services.AddScoped<ProductInterface, ProductResources>();
            services.AddScoped<ShopInterface, ShopResources>();
            services.AddScoped<ShopProductsInterface, ShopProductsResources>();
            services.AddScoped<OrderInterface, OrderResources>();
            services.AddScoped<RatingInterface, RatingResources>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication2", Version = "v1" });
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication2 v1"));
            }



            app.UseCors(x => x
             //.AllowAnyOrigin()
             .WithOrigins("http://localhost:8080")
             .AllowAnyMethod()
             .AllowAnyHeader());


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SocketHub>("/socket");
            });


        }
    }
}
