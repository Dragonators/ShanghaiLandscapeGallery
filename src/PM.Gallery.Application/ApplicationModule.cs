using PM.Gallery.Application.IServices;
using PM.Gallery.Application.Mapping;
using PM.Gallery.Application.Services;

namespace PM.Gallery.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // 注册Mapster配置
        MapsterConfig.RegisterMappings();
        // 注册应用层服务
        services.AddScoped<IImageService, ImageService>();

        // 其他应用层服务注册...
        return services;
    }
}