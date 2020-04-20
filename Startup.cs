using Bilog.Areas.Identity;
using Bilog.Data;
using Bilog.Data.Models;
using Blazored.Toast;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using WilderMinds.MetaWeblog;
using Bilog.Data;
namespace Bilog
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                  .SetBasePath(environment.ContentRootPath)
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .Build();
                    }

        public IConfiguration Configuration { get; }
        public ApplicationDbContext _context { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
          //// options.UseSqlServer(
            ///   Configuration.GetConnectionString("DefaultConnection"))
           // if (Configuration.GetConnectionString("SqlServer").Length == 0)
            //{
                services.AddDbContext<ApplicationDbContext>(options =>

                  options.UseSqlite(Configuration.GetConnectionString("Sqlite"))
                    );
           // }
           // else if (Configuration.GetConnectionString("Sqlite").Length == 0)
            {
              //  services.AddDbContext<ApplicationDbContext>(options =>

             //     options.UseSqlite(Configuration.GetConnectionString("SqlServer"))
          //          );

           }




            // services.AddDbContext<BilogContext>(options =>
            // options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(
                  options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
            services.AddServerSideBlazor()
                
                .AddCircuitOptions(options => { options.DetailedErrors = true; });

            services.AddScoped<AuthenticationStateProvider, 
                RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddScoped<BlogsService>();
            services.AddScoped<GeneralSettingsService>();
            services.AddScoped<EmailService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<SearchState>();
            services.AddMetaWeblog<MetaWeblogService>();
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddBlazoredToast();
            
            services.AddTransient<ApplicationDbContext>();
            services.AddHeadElementHelper();
         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHeadElementServerPrerendering();
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMetaWeblog("/MetaWeblog");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
      
    }
}
