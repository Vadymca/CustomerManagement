using CustomerManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IServiceCustomers, ServiceCustomers>();
            builder.Services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Customers}/{action=Index}/{id?}"
                );
            app.Run();
        }
    }
}
