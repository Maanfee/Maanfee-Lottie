using Microsoft.Extensions.DependencyInjection;

namespace Maanfee.Lottie
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMaanfeeLottie(this IServiceCollection services)
        {
            services.AddScoped<LottieService>();
            services.AddSingleton<AnimationService>();

            return services;
        }
    }
}
