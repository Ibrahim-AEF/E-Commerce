
using Domain.Contracts;
using E_Commerce.Extensions;
using E_Commerce.Factories;
using E_Commerce.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistance;
using Presistance.Data;
using Presistance.Repositories;
using Services;
using Services.Abstractions;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
            builder.Services.AddInfraStructureService(builder.Configuration);

            builder.Services.AddCoreServices();

            builder.Services.AddPresentationServices();
            #endregion
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            #region MiddleWare
            await app.SeedDbAsync();
            app.UseCustomMiddleWareExceptions();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
            #region Part 07 DbContext And Configuration

            #endregion
            #region Part 07 DbContext And Configuration

            #endregion
            #region Part 02 Product Service & Service Manager.

            #endregion
            #region Part 02 PginatedResult

            #endregion
            #region Part 02 PginatedResult

            #endregion
            #region Part 05 Basket Controller

            #endregion
            #region Part 05 Basket Controller

            #endregion
            #region Part 04 Authentication Controller

            #endregion
        }
    }
}
