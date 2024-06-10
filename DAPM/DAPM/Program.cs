using DAPM.Data;
using DAPM.Services;
using DAPM.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<DbLuLutHoaVangContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DAPM"));
});


// add services
builder.Services.AddScoped<HangHoaService>();
builder.Services.AddScoped<DanhMucService>();
builder.Services.AddScoped<UngHoService>();
builder.Services.AddScoped<DotLuService>();
builder.Services.AddScoped<DonDKService>();
builder.Services.AddScoped<ChiTietHangUngHoService>();
builder.Services.AddScoped<CuuTroService>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Product}");

app.Run();
