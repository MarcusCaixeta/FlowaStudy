using Microsoft.AspNetCore.Builder;

namespace FlowaStudy.IoC
{
    public interface IModuleInitializer
    {
        void Initialize(WebApplicationBuilder builder);
    }
}
