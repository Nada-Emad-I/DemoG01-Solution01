using DemoG01.DAL.Data.Contexts;
using DemoG01.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoG01.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionString")["DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            #endregion
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
