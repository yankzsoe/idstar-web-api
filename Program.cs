using Microsoft.Extensions.FileProviders;

namespace idstar_web_api {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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