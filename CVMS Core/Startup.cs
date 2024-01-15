

using Microsoft.Extensions.FileProviders;

namespace CVMS_Core
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            services.AddMvc();
            services.AddRazorPages();
            services.AddAuthorization();
            services.AddControllersWithViews();
            //RegisterServices(services);
            //services.AddDbContext<ELoanSystemContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ELoanSystemDB")));
        }


        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = new PathString("/Content")
            });
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HotelManagement}/{action=admin}/{id?}");
            });
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
