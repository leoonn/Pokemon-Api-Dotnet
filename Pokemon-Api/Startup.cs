using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokemon_Api.Data;
using Pokemon_Api.Services;

namespace Pokemon_Api
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
            services.AddControllersWithViews();
            services.AddDbContext<PokemonApiContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("PokemonApiContext"), builder =>
            builder.MigrationsAssembly("Pokemon-Api")));

            services.AddScoped<PokeServices>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                #region Routes for PokeApi
                endpoints.MapControllerRoute(name: "create",
                pattern: "{controller=Pokemon}/{action=Create}");
                endpoints.MapControllerRoute(name: "name",
                pattern: "{controller=Pokemon}/{action=Name}/{name?}");
                endpoints.MapControllerRoute(name: "getall",
                pattern: "{controller=Pokemon}/{action=GetAll}/{limit}/{offset}");
                endpoints.MapControllerRoute(name: "default",
                               pattern: "{controller=Home}/{action=Index}/{id?}");
                #endregion
                #region Routes for ClientsPoke
                endpoints.MapControllerRoute(name: "getmypokes",
                              pattern: "{controller=Pokemon}/{action=GetMyPokes}");
                endpoints.MapControllerRoute(name: "getmypokesbyid",
                              pattern: "{controller=Pokemon}/{action=GetMyPokesId}/{id}");
                endpoints.MapControllerRoute(name: "getmypokesbyname",
                              pattern: "{controller=Pokemon}/{action=GetMyPokesName}/{name}");
                endpoints.MapControllerRoute(name: "mypokeedit",
                              pattern: "{controller=Pokemon}/{action=MyPokeEdit}");
                endpoints.MapControllerRoute(name: "mypokedelete",
                              pattern: "{controller=Pokemon}/{action=MyPokeDelete}/{id}");
                #endregion

            });
            
        }
    }
}
