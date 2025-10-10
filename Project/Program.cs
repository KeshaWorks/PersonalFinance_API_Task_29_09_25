using FluentValidation;
using Project.Interfaces;
using Project.Repositories;
using Project.Services;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Add DI for services

            builder.Services.AddScoped<IUserManagerService, UserManagerService>();
            builder.Services.AddScoped<ILimitManagerService, LimitManagerService>();
            builder.Services.AddScoped<IRecommendationManagerService, RecommendationManagerService>();

            // Add DI for repositories

            builder.Services.AddSingleton<IUserManagerRepositorie, UserManagerRepositorie>();

            // Add DI for validators

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
