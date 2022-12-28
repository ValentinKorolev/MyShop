using Microsoft.EntityFrameworkCore;
using MyShop.ApplicationCore.Entities;
using MyShop.Configuration;
using MyShop.Infastructure.Data;
using MyShop.Interfases;
using MyShop.Services;

var builder = WebApplication.CreateBuilder(args);
MyShop.Infastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCoreServices();
builder.Services.AddScoped(typeof(IRepository<CatalogItem>), typeof(LocalCatalogItemRepository));
builder.Services.AddScoped<ICatalogItemViewModelServices, CatalogItemViewModelServices>();

var app = builder.Build();
app.Logger.LogInformation("App created");


app.Logger.LogInformation("Database migraion running...");
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<CatalogContext>();
        if (catalogContext.Database.IsSqlServer())
        {
            catalogContext.Database.Migrate();
        }
        //await CatalogContextSeed.SeedAsync(catalogContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migrations to Databse.");
    }
}

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
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.Logger.LogDebug("Starting the app... ");
app.Run();
