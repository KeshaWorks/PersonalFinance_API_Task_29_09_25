using FluentValidation;
using Project.Interfaces;
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

            // Add DI

            builder.Services.AddSingleton<IUserManagerService, UserManagerService>();
            builder.Services.AddTransient<IRecommendationManagerService, RecommendationManagerService>();
            builder.Services.AddTransient<ILimitManagerService, LimitManagerService>();

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
