﻿using FlowaStudy.IoC.ModuleInitializers;
using Microsoft.AspNetCore.Builder;

namespace FlowaStudy.IoC
{
    public static class DependencyResolver
    {
        public static void RegisterDependencies(this WebApplicationBuilder builder)
        {
            new ApplicationModuleInitializer().Initialize(builder);
            new InfrastructureModuleInitializer().Initialize(builder);
            new WebApiModuleInitializer().Initialize(builder);
        }
    }
}
