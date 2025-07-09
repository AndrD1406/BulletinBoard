
using BulletinBoard.BusinessLogic.Services;
using BulletinBoard.BusinessLogic.Services.Interfaces;
using BulletinBoard.DataAccess.Repositories;
using BulletinBoard.DataAccess.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BulletinBoard.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IDbConnection>(x =>
                new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            builder.Services.AddTransient<IAnnouncementService, AnnouncementService>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddCors(x =>
                x.AddPolicy("AllowWeb", x =>
                    x.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowWeb");
            app.MapControllers();

            app.Run();
        }
    }
}
