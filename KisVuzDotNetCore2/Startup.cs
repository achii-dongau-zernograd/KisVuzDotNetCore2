using KisVuzDotNetCore2.Controllers.Nir;
using KisVuzDotNetCore2.Controllers.Students;
using KisVuzDotNetCore2.Controllers.UchPosobiya;
using KisVuzDotNetCore2.Infrastructure;
using KisVuzDotNetCore2.Models;
using KisVuzDotNetCore2.Models.Education;
using KisVuzDotNetCore2.Models.Files;
using KisVuzDotNetCore2.Models.Nir;
using KisVuzDotNetCore2.Models.Struct;
using KisVuzDotNetCore2.Models.Students;
using KisVuzDotNetCore2.Models.UchPosobiya;
using KisVuzDotNetCore2.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KisVuzDotNetCore2
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
            string connectionString = Configuration["ConnectionStrings:ConnectionStringMySql"];
            services.AddDbContext<AppIdentityDBContext>(options => options.UseMySql(connectionString));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDBContext>();
            services.AddMvc();
            //services.AddMemoryCache();
            services.AddSession();

            services.AddTransient<IStudentRepository,StudentRepository>();
            services.AddTransient<IFileModelRepository, FileModelRepository>();
            services.AddTransient<IEduProgramRepository, EduProgramRepository>();
            services.AddTransient<IEduPlanRepository, EduPlanRepository>();
            services.AddTransient<IMetodKomissiyaRepository, MetodKomissiyaRepository>();
            services.AddTransient<IUchPosobiyaRepository, UchPosobiyaRepository>();
            services.AddTransient<ISelectListRepository, SelectListRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IPatentRepository, PatentRepository>();
            services.AddTransient<IScienceJournalRepository, ScienceJournalRepository>();
            services.AddTransient<IArticlesRepository, ArticlesRepository>();
            services.AddTransient<IStructSubvisionChiefRepository, StructSubvisionChiefRepository>();
            services.AddTransient<IMonografRepository, MonografRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseSession();

	        app.UseAuthentication();

            app.UseRequestLocalization();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });            
        }
    }
}
