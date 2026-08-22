using Order_Processing_System.Services;

namespace Order_Processing_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Allows OrderProcessingWorker to use queuestorage client directly
            builder.Services.AddSingleton<QueueStorageService>();
            builder.Services.AddSingleton<TableStorageService>();
            builder.Services.AddSingleton<BlobStorageService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Orders}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
