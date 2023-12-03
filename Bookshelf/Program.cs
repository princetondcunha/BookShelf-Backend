using Bookshelf.Models;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bookshelf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<BookshelfDbContext>(options =>
                options.UseSqlServer($"Server={Env.GetString("DB_SERVER")};Database={Env.GetString("DB_DATABASE")};User Id={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASSWORD")};TrustServerCertificate=True;"));

            var app = builder.Build();
        }
    }
}