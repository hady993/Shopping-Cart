using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.Data;
using ShoppingCart.Service.Infrastructure;
using ShoppingCart.Service.Repositories;
using ShoppingCart.Web.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext.
builder.Services.AddDbContextPool<ApplicationDbContext>
(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("ShoppingCart.Service")
));

// Add DI.
builder.Services.AddTransient<ICategory, CategoryRepo>();
builder.Services.AddTransient<IProduct, ProductRepo>();

// Add AutoMapper.
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

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
