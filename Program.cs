using BankingAPI.Repositories.Interfaces;
using BankingAPI.Repositories;
using Amazon.SimpleNotificationService;
using BankingAPI.Managers.Interfaces;
using BankingAPI.Managers;

namespace BankingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>();
            builder.Services.AddScoped<IBankAccountManager, BankAccountManager>();
            builder.Services.AddScoped<IConfiguration>();
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


            app.MapControllers();

            app.Run();
        }
    }
}
