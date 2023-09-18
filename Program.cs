using idstar_web_api.Helper;
using idstar_web_api.Interface;
using idstar_web_api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;

namespace idstar_web_api {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Ini menggunakan cara Option Pattern, jadi harus di register disini terlebih dahulu.
            builder.Services.Configure<UserProfile>(builder.Configuration.GetSection("UserProfile"));

            // Ini menggunakan cara Singleton Pattern, jadi harus di register disini terlebih dahulu.
            var userAddress = new UserAddress();
            builder.Configuration.Bind("UserAddress", userAddress);
            builder.Services.AddSingleton(userAddress);

            // Ini adalah cara menambahkan file json sekaligus me-mapping terhadap object List<Users>
            var dummyUsers = new List<Users>();
            builder.Configuration.AddJsonFile(new PhysicalFileProvider(Directory.GetCurrentDirectory()),
                   "DummyData.json", optional: false, reloadOnChange: true).Build().GetSection("UsersData").Bind(dummyUsers);
            builder.Services.AddSingleton(dummyUsers);

            // Add services to the container.
            builder.Services.AddSingleton<INumberGenerator, SequenceNumberService>();
            builder.Services.AddSingleton<ISingletonGenerator, SingletonService>();
            builder.Services.AddScoped<IScopedGenerator, ScopedService>();
            builder.Services.AddTransient<ITransientGenerator, SampleTransientService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Untuk menambahkan middleware pengiriman file statis ke dalam pipeline middleware ASP.NET Core
            app.UseStaticFiles(new StaticFileOptions {
                // Ini adalah objek yang memberi tahu ASP.NET Core dari mana file-file statis akan diambil.
                // Dalam kasus ini, kita menggunakan PhysicalFileProvider, yang mengacu pada lokasi fisik di sistem file.
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Assets")),
                // Ini adalah path yang digunakan untuk mengakses file-file statis dalam URL
                RequestPath = "/Assets"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}