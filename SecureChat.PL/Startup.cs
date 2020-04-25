using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecureChat.BLL.Repository;
using SecureChat.Core.Interfaces;
using SecureChat.DAL;
using SecureChat.DAL.Contexts;
using SecureChat.DAL.Models;

namespace SecureChat.PL
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
            Console.WriteLine(Configuration);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var identityConString = Configuration["Data:SecureChatIdentity:ConnectionString"];
            var messagesConString = Configuration["Data:SecureChatMessages:ConnectionString"];
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(identityConString));
            services.AddDbContext<MessagesDBContext>(options => options.UseSqlServer(messagesConString));
            services.AddTransient<IRepository<User>,UserRepository>();
            services.AddTransient<IRepository<IMessage>,MessageRepository>();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: null,
                  template: "{user}",
                  defaults: new { controller = "User", action = "List" }
                );
                routes.MapRoute(
                   name: "default",
                   template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
