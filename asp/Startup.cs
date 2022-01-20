
using asp.DataContext;
using asp.DataContext.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp
{
    public class Startup
    {
     

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) 
        {
            //services.AddDbContext<NoteDbcontext>(options =>
            //{
            //    options.UseSqlServer(_config.GetConnectionString("AspConnection"));
            //});
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddSession(); // 세션 기능 사용
            var AspConnection = @"Server=kuniv-practice.database.windows.net;Database=kuniv-practice;Trusted_Connection=True;MultipleActiveResultSets=true;";
            services.AddDbContext<NoteDbcontext>(options => options.UseSqlServer(AspConnection));



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
            app.UseSession(); //어플리케이션에 세션 기능 사용
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
