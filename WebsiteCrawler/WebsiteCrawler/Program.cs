using DataAccess.DataContext;
using DataAccess.Repo;
using Microsoft.EntityFrameworkCore;

namespace WebsiteCrawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CrawlerDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("sql"));
            });

            builder.Services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Users}/{action=Login}/{id?}");

            //app.MapRazorPages();

            app.Run();
        }
    }
}