using MyShop.Models;
using MyShop.DAL;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ItemDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ItemDbContextConnection"]);
});

builder.Services.AddScoped<IItemRepository, ItemRepository>();

var loggerConfig = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File($"logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");
var logger = loggerConfig.CreateLogger();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DbInit.Seed(app);
}

app.UseStaticFiles();

app.MapDefaultControllerRoute();

app.Run();

