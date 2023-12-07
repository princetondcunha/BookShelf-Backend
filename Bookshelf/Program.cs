using Bookshelf.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookshelfDbContext>(options =>
    options.UseSqlServer($"Server={Env.GetString("DB_SERVER")};Database={Env.GetString("DB_DATABASE")};User Id={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASSWORD")};TrustServerCertificate=True;"));

builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();
//cors
app.UseCors("AllowAll");


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
// Use authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();