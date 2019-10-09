using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Debit
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var urls = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()["AllowCors:AllowAllOrigin"].Split(",");

            services.AddControllers();

            services.AddDistributedMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("any",
                    builder => builder.WithOrigins(urls)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSession(options =>
            {
                //设置session过期时间为15min
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = false;
                options.Cookie.Name = "Ygb.Session";
                options.Cookie.SameSite = SameSiteMode.None;
            });
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

            app.UseSession();

            app.UseCors("any");

            app.UseHttpsRedirection()
                .UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
            });
        }
    }
}
