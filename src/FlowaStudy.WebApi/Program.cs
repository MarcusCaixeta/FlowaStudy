using FlowaStudy.Application;
using FlowaStudy.IoC;
using FlowaStudy.ORM.Cache;
using FlowaStudy.ORM.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;

namespace FlowaStudy.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            //builder.AddDefaultLogging();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();         

            //builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();
            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            var app = builder.Build();
            //app.UseMiddleware<ValidationExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {

            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
