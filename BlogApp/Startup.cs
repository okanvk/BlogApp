
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data.Concrete.EFCore;
using BlogApp.Data.Abstract;
using BlogApp.IdentityCore;
using Microsoft.AspNetCore.Identity;
using BlogApp.UI.Migrations;

namespace BlogApp.UI
{
    public class Startup
    {

        IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration=configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

           services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),b => b.MigrationsAssembly("BlogApp.UI")));
           services.AddDbContext<BlogContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),b => b.MigrationsAssembly("BlogApp.UI")));
           services.AddIdentity<ApplicationUser,IdentityRole>()
           .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
           .AddDefaultTokenProviders();

            services.AddTransient<IMessageRepository,EfMessageRepository>();
            services.AddTransient<IBlogRepository,EfBlogRepository>();
            services.AddTransient<ICategoryRepository,EfCategoryRepository>();
            services.AddTransient<ICommentRepository,EfCommentRepository>();
            services.AddMvc();
            services.AddSession();
            services.AddMemoryCache();
            //var connectionString = @"server=localhost;database=blogdb;uid=sa;pwd=PaSS22Me";  Eski versiyon
             // services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("BlogApp.UI")));


          
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
      //      app.UseStatusCodePages();

            app.UseAuthentication();

            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    name:"default",
                    template:"{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name:"second",
                    template:"{controller=Account}/{action=Login}"
                );

                routes.MapRoute(
                    name:"third",
                    template:"{controller=Category}/{action=List}"
                );


            });

            SeedData.Seed(app);
            SeedIdentity.CreateIdentityUsers(app.ApplicationServices,configuration).Wait();
        }
    }
}
