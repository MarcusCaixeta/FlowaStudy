using FlowaStudy.Domain.Common.Interfaces;
using FlowaStudy.Domain.Repositories;
using FlowaStudy.ORM.Cache;
using FlowaStudy.ORM.Contexts;
using FlowaStudy.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace FlowaStudy.IoC.ModuleInitializers
{
    public class InfrastructureModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<EfContext>());

            builder.Services.AddScoped<ICacheService, RedisCacheService>();

            builder.Services.AddScoped<ICacheService, RedisCacheService>();
            builder.Services.AddScoped<IFinancialAssetRepository, FinancialAssetRepository>();
        }
    }
   
}
