using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Mathe_Nachhilfe_Plattform.Models;
using Mathe_Nachhilfe_Plattform.Models.Respositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Mathe_Nachhilfe_Plattform
{
    public class Startup
    {
        private readonly IConfiguration configuration; 
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
       

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc();
            services.AddScoped<IDokumentstoreRepository<user>, UserDbRepository>();
            services.AddScoped<IDokumentstoreRepository<dokument>,DokumentDbRepository>();
            services.AddDbContext<dokumentstorDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlCon"));
               

            }
            
            ); 
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
       
    }
}
