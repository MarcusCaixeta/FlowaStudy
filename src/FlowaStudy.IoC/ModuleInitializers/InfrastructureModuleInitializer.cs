using FlowaStudy.Application.Messaging.Handler;
using FlowaStudy.Application.Messaging.Interfaces;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Common.Interfaces.Services;
using FlowaStudy.Domain.Entities;
using FlowaStudy.Messaging.Service;
using FlowaStudy.ORM.Cache;
using FlowaStudy.ORM.Configuration.MongoDb;
using FlowaStudy.ORM.Contexts;
using FlowaStudy.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using StackExchange.Redis;

namespace FlowaStudy.IoC.ModuleInitializers
{
    public class InfrastructureModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EfContext>(options =>
               options.UseNpgsql(
                   builder.Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly("FlowaStudy.ORM")
               )
           );

            builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<EfContext>());


            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = builder.Configuration.GetValue<string>("Redis:ConnectionString");
                return ConnectionMultiplexer.Connect(configuration!);
            });
            builder.Services.AddScoped<ICacheService, RedisCacheService>();

            builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

            builder.Services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);

                BsonClassMap.RegisterClassMap<FinancialAssetMongo>(cm =>
                {
                    cm.AutoMap();
                    cm.MapProperty(c => c.Id)
                        .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));

                    cm.MapProperty(c => c.Name).SetIsRequired(true);  
                    cm.MapProperty(c => c.Value).SetIsRequired(true);  
                    cm.MapProperty(c => c.AcquisitionDate).SetIsRequired(true); 
                });

                return database;
            });

            builder.Services.AddSingleton<IKafkaProducer, KafkaProducerService>();
            builder.Services.AddSingleton<IKafkaConsumerHandler, ProcessMessageHandler>(); // Implementação custom
            builder.Services.AddHostedService<KafkaConsumerHostedService>();

            builder.Services.AddScoped<IFinancialAssetRepositoryMongo, FinancialAssetRepositoryMongo>();

            builder.Services.AddScoped<IFinancialAssetRepository, FinancialAssetRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAssetRepository, AssetRepository>();
            builder.Services.AddScoped<IAssetTransactionRepository, AssetTransactionRepository>();
        }
    }

}
