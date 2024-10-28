using Application.Interfaces;
using Application.Service;
using Domain.Stores;
using Infrastructure;
using Persistence.Repositories;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using Serilog.Events;
using Serilog.Sinks.GrafanaLoki;
using API.Middleware;
using Application.Services;

namespace ShippingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var credentials = new GrafanaLokiCredentials()
            {
                User = builder.Configuration.GetSection("LokiOptions").GetValue<string>("User")
                ?? throw new ArgumentNullException("Loki User"),
                Password = builder.Configuration.GetSection("LokiOptions").GetValue<string>("Password") 
                ?? throw new ArgumentNullException("Loki Password")
            };
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.GrafanaLoki(
                    builder.Configuration.GetSection("LokiOptions").GetValue<string>("URI") 
                    ?? throw new ArgumentNullException("Loki URI"),
                    credentials,
                    new Dictionary<string, string> { { "app", "Serilog.Sinks.GrafanaLoki.Sample" } },
                    LogEventLevel.Information
                ).CreateLogger();
            builder.Host.UseSerilog();
            builder.Logging.AddSerilog();

            builder.Services.Configure<MongoOptions>(builder.Configuration.GetSection(MongoOptions.OptionsName));

            builder.Services.AddSingleton<ShippingContext>();
            builder.Services.AddScoped<IOrderStore, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IResultStore, ResultRepository>();
            builder.Services.AddScoped<IResultService, ResultService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "APIDocumentation.xml"));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
